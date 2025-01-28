using Aretech.Application.Accounts.Commands.Login;
using Aretech.Application.Common;
using Aretech.Application.SeedWork;
using Aretech.Core;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.SeedWorks.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Aretech.Application.Accounts.Commands.RefreshToken
{
	public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<LoginResponse>>
	{
		private readonly ITokenService _tokenService;
		private readonly IAccountService _accountService;
		private readonly IConfiguration _configuration;
		public RefreshTokenCommandHandler(
						ITokenService tokenService,
						IAccountService accountService,
						IConfiguration configuration
			)
		{
			_tokenService = tokenService;
			_accountService = accountService;
			_configuration = configuration;
		}

		public async Task<ApiResponse<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
			var username = principal.Identity.Name;

			var account = await _accountService.GetAccountByUserNameAsync(username);
			if (account == null || account.RefreshToken != request.RefreshToken || account.RefreshTokenExpiryTime <= DateTime.Now)
			{
				throw new AretechException("Geçersiz refresh token.", HttpStatusCode.Unauthorized);
			}

			var newAccessToken = _tokenService.GenerateToken(account);
			var newRefreshToken = _tokenService.GenerateRefreshToken();

			account.RefreshToken = newRefreshToken;
			account.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:RefreshTokenExpirationDays"]));
			await _accountService.UpdateAsync(account);

			return new ApiResponse<LoginResponse>
			{
				Success = true,
				Message = MessageConstant.Success,
				Data = new LoginResponse
				{
					AccessToken = newAccessToken,
					RefreshToken = newRefreshToken
				}
			};
		}
	}
}