using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsQuery : IRequest<ApiResponse<List<GetAccountsResponse>>> { }
}