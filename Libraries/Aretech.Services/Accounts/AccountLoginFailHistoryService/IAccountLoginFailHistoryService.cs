using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts.AccountLoginFailHistoryService
{
	public interface IAccountLoginFailHistoryService
	{
		Task<List<AccountLoginFailHistory>> GetAccountsAsync(CancellationToken cancellationToken = default);
		Task<int> AddAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
	}
}