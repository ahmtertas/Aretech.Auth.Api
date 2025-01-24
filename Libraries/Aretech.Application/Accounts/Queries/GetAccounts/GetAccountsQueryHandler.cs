using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ApiResponse<List<GetAccountsResponse>>>
	{
		public GetAccountsQueryHandler()
		{

		}
		public async Task<ApiResponse<List<GetAccountsResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}