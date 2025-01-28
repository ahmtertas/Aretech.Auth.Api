using Aretech.Auth.Web.Api.Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using System.Text.Json;
using static Aretech.Auth.Web.Api.Framework.Middlewares.ExceptionHandlerMiddleware;

public class RateLimitingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly List<RateLimitingRule> _rules;
	private static readonly Dictionary<string, (int Count, DateTime Expiry)> _requestCounts = new();

	public RateLimitingMiddleware(RequestDelegate next, List<RateLimitingRule> rules)
	{
		_next = next;
		_rules = rules;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var ip = context.Connection.RemoteIpAddress?.ToString();
		var path = context.Request.Path;
		var method = context.Request.Method;

		var rule = _rules.FirstOrDefault(r =>
										r.Endpoint.Contains(path.ToString(), StringComparison.OrdinalIgnoreCase) &&
										r.Method.Contains(method, StringComparison.OrdinalIgnoreCase));


		if (rule is null)
			rule = _rules.FirstOrDefault(x => x.Endpoint.Equals("*") && x.Method.Equals("*"));

		var key = $"{ip}:{path}:{method}";

		if (_requestCounts.TryGetValue(key, out var requestInfo))
		{
			if (requestInfo.Expiry < DateTime.Now)
			{
				_requestCounts[key] = (1, DateTime.Now.Add(ParsePeriod(rule.Period)));
			}
			else if (requestInfo.Count >= rule.Limit)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var detail = new ErrorDetail
				{
					Message = "Rate limit exceeded. Try again later.",
					Instance = context.Request.Path,
					Type = context.Request.GetDisplayUrl(),
					RootUrl = $"{context.Request.Scheme}://{context.Request.Host}",
				};

				response.StatusCode = (int)HttpStatusCode.TooManyRequests;
				detail.StatusCode = (int)HttpStatusCode.TooManyRequests;
				var result = JsonSerializer.Serialize(detail);
				await response.WriteAsync(result);
				return;
			}
			else
			{
				_requestCounts[key] = (requestInfo.Count + 1, requestInfo.Expiry);
			}
		}
		else
		{
			_requestCounts[key] = (1, DateTime.Now.Add(ParsePeriod(rule.Period)));
		}

		await _next(context);
	}

	private TimeSpan ParsePeriod(string period)
	{
		if (period.EndsWith("s")) return TimeSpan.FromSeconds(int.Parse(period[..^1]));
		if (period.EndsWith("m")) return TimeSpan.FromMinutes(int.Parse(period[..^1]));
		if (period.EndsWith("h")) return TimeSpan.FromHours(int.Parse(period[..^1]));
		throw new ArgumentException("Invalid period format.");
	}
}