using Aretech.Application.Accounts.Commands.CreateAccount;
using Aretech.Application.Accounts.Commands.ForgotPassword;
using Aretech.Application.Accounts.Commands.Login;
using Aretech.Application.Accounts.Commands.Logout;
using Aretech.Application.Accounts.Commands.RefreshToken;
using Aretech.Application.Accounts.Queries.GetAccounts;
using Aretech.Application.SeedWork;
using Aretech.Auth.Web.Api.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;



namespace Aretech.Auth.Web.Api.Controllers.Accounts
{
	[Authorize]
	[ApiController]
	[Route("api/auth/account")]
	public class AccountsController : BaseController
	{
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpGet]
		public async Task<ApiResponse<List<GetAccountsResponse>>> GetAccounts(CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(new GetAccountsQuery(), cancellationToken);
			return response;
		}

		[AllowAnonymous]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpPost]
		public async Task<ApiResponse<CreateAccountResponse>> CreateAccount([FromBody] CreateAccountCommand request, CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);
			return response;
		}

		[AllowAnonymous]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpPost("login")]
		public async Task<ApiResponse<LoginResponse>> Login([FromBody] LoginCommand request, CancellationToken cancellation = default)
		{
			var response = await Mediator.Send(request);
			return response;
		}

		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpPost("logout")]
		public async Task<ApiResponse<bool>> Logout(CancellationToken cancellation = default)
		{
			var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadJwtToken(token);
			var accountId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
			var logoutCommand = new LogoutCommand { AccountId = accountId };
			var response = await Mediator.Send(logoutCommand);
			return response;
		}

		[AllowAnonymous]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpPost("refresh-token")]
		public async Task<ApiResponse<LoginResponse>> Refresh([FromBody] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return response;
		}

		[AllowAnonymous]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpPost("forgot-password")]
		public async Task<ApiResponse<bool>> ForgotPassword([FromBody] ForgotPasswordCommand command)
		{
			var response = await Mediator.Send(command);
			return response;
		}
	}
}