using Aretech.Domain.Accounts;

namespace Aretech.Services.Accounts.PasswordHistoryService
{
	public interface IPasswordHistoryService
	{
		Task<int> AddAsync(PasswordHistory passwordHistory);
	}
}
