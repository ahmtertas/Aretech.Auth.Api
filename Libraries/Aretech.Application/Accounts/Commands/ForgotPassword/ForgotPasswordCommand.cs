using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Commands.ForgotPassword
{
	public class ForgotPasswordCommand : IRequest<ApiResponse<bool>>
	{
		public string UserName { get; set; } = default!;
	}
}