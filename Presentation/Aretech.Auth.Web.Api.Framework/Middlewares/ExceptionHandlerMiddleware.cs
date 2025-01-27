using Aretech.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Aretech.Auth.Web.Api.Framework.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (AretechException arex)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var detail = new ErrorDetail
				{
					Message = arex.Message,
					Detail = GetFullException(arex),
					Instance = context.Request.Path,
					Type = context.Request.GetDisplayUrl(),
					RootUrl = $"{context.Request.Scheme}://{context.Request.Host}",
				};

				response.StatusCode = arex.StatusCode ?? (int)HttpStatusCode.BadRequest;
				detail.StatusCode = arex.StatusCode ?? (int)HttpStatusCode.BadRequest;
				var result = JsonSerializer.Serialize(detail);
				await response.WriteAsync(result);
			}
			catch (Exception ex)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var detail = new ErrorDetail
				{
					Message = ex.Message,
					Detail = GetFullException(ex),
					Instance = context.Request.Path,
					Type = context.Request.GetDisplayUrl(),
					RootUrl = $"{context.Request.Scheme}://{context.Request.Host}",
				};

				switch (ex)
				{
					case AretechException:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					case KeyNotFoundException:
						response.StatusCode = (int)HttpStatusCode.NotFound;
						break;

					default:
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}

				var result = JsonSerializer.Serialize(detail);
				await response.WriteAsync(result);
			}
		}


		private string GetFullException(Exception ex)
		{
			var messages = new StringBuilder();
			var currentException = ex;
			while (currentException != null)
			{
				messages.AppendLine(currentException.ToString());
				currentException = currentException.InnerException;
			}
			return messages.ToString();
		}

		public sealed class ErrorDetail
		{
			public int StatusCode { get; set; }
			public string Application { get; set; } = "Aretech.Auth.Api";
			public string RootUrl { get; set; } = null!;
			public string Instance { get; set; } = null!;
			public string Type { get; set; } = null!;
			public string Message { get; set; } = null!;
			public string Detail { get; set; } = null!;
		}
	}
}
