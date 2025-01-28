using Aretech.Application.Common;
using Aretech.Application.SeedWork;
using Aretech.Services.Accounts.AccountsService;
using MediatR;

namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ApiResponse<List<GetAccountsResponse>>>
	{
		private readonly IAccountService _accountService;
		public GetAccountsQueryHandler(IAccountService accountService)
		{
			_accountService = accountService;
		}
		public async Task<ApiResponse<List<GetAccountsResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
		{
			var response = await _accountService.GetAccountsAsync();
			return new ApiResponse<List<GetAccountsResponse>>()
			{
				Data = [],
				Success = true,
				Message = MessageConstant.Success
			};
		}
	}
}