using Aretech.Application.Accounts.Commands.CreateAccount;
using Aretech.Application.Accounts.Commands.Login;
using Aretech.Application.Accounts.Commands.RefreshToken;
using Aretech.Application.Accounts.Queries.GetAccounts;
using Aretech.Application.SeedWork;
using Aretech.Auth.Web.Api.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aretech.Auth.Web.Api.Controllers.Accounts
{
	[Authorize]
	[ApiController]
	[Route("api/account")]
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
	}
}