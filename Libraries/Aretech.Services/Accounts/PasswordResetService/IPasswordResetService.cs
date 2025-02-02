namespace Aretech.Services.Accounts.PasswordResetService
{
	public interface IPasswordResetService
	{
		Task<int> AddPasswordResetAsync();
		Task<bool> ValidatePasswordResetToken(string token, DateTime expiry);
	}
}
