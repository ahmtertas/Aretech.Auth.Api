using Aretech.Application.Common;
using Aretech.Application.SeedWork;
using Aretech.Core;
using Aretech.Infrastructure.Abstract;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.PasswordResetService;
using MediatR;

namespace Aretech.Application.Accounts.Commands.ForgotPassword
{
	public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<bool>>
	{
		private readonly IAccountService _accountService;
		private readonly IEmailService _emailService;
		private readonly IPasswordResetService _passwordResetService;

		public ForgotPasswordCommandHandler(IAccountService accountService, IEmailService emailService, IPasswordResetService passwordResetService)
		{
			_accountService = accountService;
			_emailService = emailService;
			_passwordResetService = passwordResetService;
		}

		public async Task<ApiResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var account = await _accountService.GetAccountByUserNameAsync(request.UserName);
			if (account is null)
				throw new AretechException("Kullanıcı bulunamadı.", System.Net.HttpStatusCode.BadRequest);

			var token = _passwordResetService.AddPasswordResetAsync();

			var resetLink = $"http://localhost/reset-password?token={token}";
			var emailBody = $"Please reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>";
			await _emailService.SendEmailAsync(account.Email, "Password Reset", emailBody);

			return new ApiResponse<bool>()
			{
				Success = true,
				Message = MessageConstant.Success,
				Data = true
			};
		}
	}
}
