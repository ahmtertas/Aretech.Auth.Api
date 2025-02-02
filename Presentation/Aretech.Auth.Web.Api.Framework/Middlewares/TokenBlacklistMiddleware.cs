using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.BlackListedTokenService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using static Aretech.Auth.Web.Api.Framework.Middlewares.ExceptionHandlerMiddleware;

namespace Aretech.Auth.Web.Api.Framework.Middlewares
{
	public class TokenBlacklistMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IServiceScopeFactory _scopeFactory;

		public TokenBlacklistMiddleware
			(
						 RequestDelegate next,
						 IServiceScopeFactory scopeFactory
			)
		{
			_next = next;
			_scopeFactory = scopeFactory;
		}

		public async Task Invoke(HttpContext context)
		{
			using var scope = _scopeFactory.CreateScope();
			var _blackListedTokenService = scope.ServiceProvider.GetRequiredService<IBlackListedTokenService>();
			var _accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();


			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			if (token is null)
			{
				await _next(context);
				return;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadJwtToken(token);

			var accountId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
			ArgumentNullException.ThrowIfNull(accountId);

			var account = await _accountService.GetAccountByIdAsync(Guid.Parse(accountId));
			ArgumentNullException.ThrowIfNull(account);

			var blacklistedToken = await _blackListedTokenService.GetBlacklistedTokenByTokenAndAccountIdAsync(account.RefreshToken, account.Id);

			if (blacklistedToken is not null)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var detail = new ErrorDetail
				{
					Message = "Token is blacklisted.",
					Instance = context.Request.Path,
					Type = context.Request.GetDisplayUrl(),
					RootUrl = $"{context.Request.Scheme}://{context.Request.Host}",
				};

				response.StatusCode = StatusCodes.Status401Unauthorized;
				detail.StatusCode = StatusCodes.Status401Unauthorized;
				var result = JsonSerializer.Serialize(detail);
				await response.WriteAsync(result);
				return;
			}

			await _next(context);
		}
	}
}