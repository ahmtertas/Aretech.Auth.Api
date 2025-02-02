using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts.BlackListedTokenService
{
	public interface IBlackListedTokenService
	{
		Task<int> AddAsync(BlacklistedToken blacklistedToken, CancellationToken cancellationToken = default);
		Task<BlacklistedToken?> GetBlacklistedTokenByTokenAndAccountIdAsync(string? token, Guid accountId, CancellationToken cancellationToken = default);
	}
}