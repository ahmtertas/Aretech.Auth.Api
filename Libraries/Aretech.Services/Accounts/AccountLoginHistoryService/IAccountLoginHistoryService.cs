using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts.AccountLoginHistoryService
{
	public interface IAccountLoginHistoryService
	{
		Task<List<AccountLoginHistory>> GetAccountsAsync(CancellationToken cancellationToken = default);
		Task<int> AddAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
	}
}