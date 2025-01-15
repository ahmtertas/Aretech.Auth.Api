using Aretech.Core;
using Microsoft.AspNetCore.Http;

namespace Aretech.Auth.Web.Api.Framework.Middlewares
{
	public class AuthenticationContextMiddleware
	{
		private readonly RequestDelegate _next;

		public AuthenticationContextMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context,
								 IAuthenticationContext authenticationContext)
		{

		}
	}
}