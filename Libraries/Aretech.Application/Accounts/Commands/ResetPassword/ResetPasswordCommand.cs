using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Commands.ResetPassword
{
	public class ResetPasswordCommand : IRequest<ApiResponse<bool>>
	{
		public string Token { get; set; } = default!;
		public string NewPassword { get; set; } = default!;
	}
}