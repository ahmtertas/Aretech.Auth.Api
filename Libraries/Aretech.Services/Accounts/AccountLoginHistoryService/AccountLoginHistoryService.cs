using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Services.Accounts.AccountLoginHistoryService
{
	public class AccountLoginHistoryService : IAccountLoginHistoryService
	{
		private readonly IRepository<AccountLoginHistory> _accountLoginHistoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AccountLoginHistoryService
			(
						 IRepository<AccountLoginHistory> accountLoginHistoryRepository,
						 IUnitOfWork unitOfWork
			)
		{
			_accountLoginHistoryRepository = accountLoginHistoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<int> AddAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginHistory));

			await _accountLoginHistoryRepository.AddAsync(accountLoginHistory, cancellationToken);
			return await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginHistory));

			_accountLoginHistoryRepository.Delete(accountLoginHistory);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));

			var accountLoginHistory = await _accountLoginHistoryRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			return await DeleteAsync(accountLoginHistory);
		}

		public async Task<List<AccountLoginHistory>> GetAccountsAsync(CancellationToken cancellationToken = default)
		{
			var accountLoginHistories = await _accountLoginHistoryRepository.TableNoTracking.ToListAsync(cancellationToken);
			return accountLoginHistories;
		}

		public async Task<bool> UpdateAsync(AccountLoginHistory accountLoginHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginHistory));

			_accountLoginHistoryRepository.Update(accountLoginHistory);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));

			var accountLoginHistory = await _accountLoginHistoryRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			return await UpdateAsync(accountLoginHistory, cancellationToken);
		}
	}
}