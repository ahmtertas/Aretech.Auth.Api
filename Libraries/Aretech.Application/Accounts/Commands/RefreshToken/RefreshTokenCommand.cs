using Aretech.Application.Accounts.Commands.Login;
using Aretech.Application.SeedWork;
using MediatR;

namespace Aretech.Application.Accounts.Commands.RefreshToken
{
	public class RefreshTokenCommand : IRequest<ApiResponse<LoginResponse>>
	{
		public string AccessToken { get; set; } = null!;
		public string RefreshToken { get; set; } = null!;
	}
}
