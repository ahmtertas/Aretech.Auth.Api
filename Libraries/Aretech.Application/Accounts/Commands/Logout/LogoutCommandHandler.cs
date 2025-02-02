using Aretech.Application.Common;
using Aretech.Application.SeedWork;
using Aretech.Domain.Accounts;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.BlackListedTokenService;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Aretech.Application.Accounts.Commands.Logout
{
	public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
	{
		private readonly IBlackListedTokenService _blackListedTokenService;
		private readonly IConfiguration _configuration;
		private readonly IAccountService _accountService;
		public LogoutCommandHandler(IBlackListedTokenService blackListedTokenService, IConfiguration configuration, IAccountService accountService)
		{
			_blackListedTokenService = blackListedTokenService;
			_configuration = configuration;
			_accountService = accountService;
		}

		public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			var account = await _accountService.GetAccountByIdAsync(Guid.Parse(request.AccountId));
			ArgumentNullException.ThrowIfNull(account);

			var token = new BlacklistedToken
			{
				Token = account.RefreshToken,
				AccountId = Guid.Parse(request.AccountId),
				ExpiryDate = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:RefreshTokenExpirationDays"]))
			};

			token.SetCreatedBy(Guid.Parse(request.AccountId));
			await _blackListedTokenService.AddAsync(token);
			return new ApiResponse<bool>
			{
				Success = true,
				Message = MessageConstant.Success,
				Data = true
			};
		}
	}
}