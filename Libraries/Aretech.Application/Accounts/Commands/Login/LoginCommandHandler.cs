using Aretech.Application.Common;
using Aretech.Application.Mapping;
using Aretech.Application.SeedWork;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.Models;
using Aretech.Services.SeedWorks.Security;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Aretech.Application.Accounts.Commands.Login
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponse>>
	{
		private readonly IAccountService _accountService;
		private readonly IMappingService _mappingService;
		private readonly ITokenService _tokenService;
		private readonly IConfiguration _config;
		public LoginCommandHandler
			(
						 IAccountService accountService,
						 IMappingService mappingService,
						 ITokenService tokenService,
						 IConfiguration config
			)
		{
			_accountService = accountService;
			_mappingService = mappingService;
			_tokenService = tokenService;
			_config = config;
		}
		public async Task<ApiResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(nameof(request), "LoginCommand request is null.");

			var loginModel = _mappingService.Map<LoginCommand, LoginModel>(request);
			var accessToken = await _accountService.LoginAsync(loginModel);
			var refreshToken = _tokenService.GenerateRefreshToken();

			var account = await _accountService.GetAccountByUserNameAsync(request.UserName);
			ArgumentNullException.ThrowIfNull(nameof(account), "Account is null.");
			account.RefreshToken = refreshToken;
			account.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(_config["Jwt:RefreshTokenExpirationDays"]));
			await _accountService.UpdateAsync(account);

			return new ApiResponse<LoginResponse>()
			{
				Message = MessageConstant.Success,
				Success = true,
				Data = new LoginResponse()
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken
				},
			};
		}
	}
}