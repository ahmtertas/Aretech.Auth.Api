using Aretech.Domain.Accounts;
using Aretech.Services.Accounts.Models;

namespace Aretech.Services.Accounts.AccountsService
{
	public interface IAccountService
	{
		Task<List<Account>> GetAccountsAsync(CancellationToken cancellationToken = default);
		Task<int> AddAsync(Account account, CancellationToken cancellationToken = default);
		Task<bool> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(Account account, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
	}
}