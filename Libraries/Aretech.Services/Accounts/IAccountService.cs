using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts
{
	public interface IAccountService
	{
		Task<List<Account>> GetAccountsAsync();
		Task<int> AddAsync(Account account);
		bool Update(Account account);
		bool Update(Guid id);
		bool Delete(Account account);
		bool Delete(Guid id);
	}
}