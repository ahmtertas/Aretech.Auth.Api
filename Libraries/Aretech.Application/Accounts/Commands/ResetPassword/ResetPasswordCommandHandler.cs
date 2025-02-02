using Aretech.Application.Common;
using Aretech.Application.SeedWork;
using Aretech.Core;
using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.PasswordHistoryService;
using Aretech.Services.Accounts.PasswordResetService;
using MediatR;

namespace Aretech.Application.Accounts.Commands.ResetPassword
{
	public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
	{
		private readonly IAccountService _accountService;
		private readonly IPasswordResetService _passwordResetService;
		private readonly IHashService _hashService;
		private readonly IPasswordHistoryService _passwordHistoryService;

		public ResetPasswordCommandHandler
			(
						 IPasswordResetService passwordResetService,
						 IAccountService accountService,
						 IHashService hashService,
						 IPasswordHistoryService passwordHistoryService
			)
		{
			_passwordResetService = passwordResetService;
			_accountService = accountService;
			_hashService = hashService;
			_passwordHistoryService = passwordHistoryService;
		}

		public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var passwordReset = await _passwordResetService.ValidatePasswordResetToken(request.Token);

			if (passwordReset is null)
				throw new AretechException("Hatalı token.", System.Net.HttpStatusCode.BadRequest);

			var account = await _accountService.GetAccountByIdAsync(passwordReset.AccountId);

			if (account is null)
				throw new AretechException("Hesap bulunamadı.", System.Net.HttpStatusCode.BadRequest);

			var olderPasswordHash = account.PasswordHash;

			account.PasswordHash = _hashService.HashPassword(request.NewPassword);
			account.RefreshToken = null;
			account.RefreshTokenExpiryTime = null;
			await _accountService.UpdateAsync(account);

			var passwordHistory = new PasswordHistory
			{
				AccountId = account.Id,
				OldPasswordHash = olderPasswordHash,
			};

			passwordHistory.SetCreatedBy(account.Id);
			await _passwordHistoryService.AddAsync(passwordHistory);


			return new ApiResponse<bool>()
			{
				Message = MessageConstant.Success,
				Success = true,
				Data = true
			};
		}
	}
}