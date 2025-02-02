using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Commands.Logout
{
	public class LogoutCommand : IRequest<ApiResponse<bool>>
	{
		public string AccountId { get; set; } = default!;
	}
}