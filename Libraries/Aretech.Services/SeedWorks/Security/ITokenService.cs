using Aretech.Domain.Accounts;
using System.Security.Claims;

namespace Aretech.Services.SeedWorks.Security
{
	public interface ITokenService
	{
		string GenerateToken(Account account);
		bool ValidateToken(string token);
		string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}