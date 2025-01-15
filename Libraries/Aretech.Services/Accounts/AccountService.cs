using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts
{
	public class AccountService : IAccountService
	{

		public AccountService()
		{

		}
		public Task<int> AddAsync(Account account)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Account account)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Account>> GetAccountsAsync()
		{
			throw new NotImplementedException();
		}

		public bool Update(Account account)
		{
			throw new NotImplementedException();
		}

		public bool Update(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
