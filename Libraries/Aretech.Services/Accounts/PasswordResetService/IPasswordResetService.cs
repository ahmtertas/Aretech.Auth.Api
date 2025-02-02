using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts.PasswordResetService
{
	public interface IPasswordResetService
	{
		Task<string> AddPasswordResetAsync(Guid accountId);
		Task<PasswordReset?> ValidatePasswordResetToken(string token);
	}
}