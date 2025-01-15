using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsQuery : IRequest<BaseEndPointResponse<List<GetAccountsResponse>>> { }
}