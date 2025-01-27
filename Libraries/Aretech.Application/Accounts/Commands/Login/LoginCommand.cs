using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Commands.Login
{
	public class LoginCommand : IRequest<ApiResponse<LoginResponse>>
	{
		public string UserName { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}