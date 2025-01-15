using Aretech.Application.Accounts.Queries.GetAccounts;
using Aretech.Application.SeedWork;
using Aretech.Auth.Web.Api.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Aretech.Auth.Web.Api.Controllers
{
	[ApiController]
	[Route("accounts")]
	public class AccountsController : BaseController
	{
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
		[HttpGet("get-accounts")]
		public async Task<BaseEndPointResponse<List<GetAccountsResponse>>> GetAccounts(CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(new GetAccountsQuery(), cancellationToken);
			return response;
		}
	}
}