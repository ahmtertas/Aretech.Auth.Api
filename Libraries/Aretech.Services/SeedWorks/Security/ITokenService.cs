using Aretech.Domain.Accounts;

namespace Aretech.Services.SeedWorks.Security
{
	public interface ITokenService
	{
		string GenerateToken(Account account);
		bool ValidateToken(string token);
	}
}