using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, BaseEndPointResponse<List<GetAccountsResponse>>>
	{
		public GetAccountsHandler()
		{

		}
		public async Task<BaseEndPointResponse<List<GetAccountsResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}