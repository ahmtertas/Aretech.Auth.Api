using Aretech.Core;
using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Services.Accounts.AccountLoginFailHistoryService
{
	public class AccountLoginFailHistoryService : IAccountLoginFailHistoryService
	{

		private readonly IRepository<AccountLoginFailHistory> _accountLoginFailHistoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AccountLoginFailHistoryService(IRepository<AccountLoginFailHistory> accountLoginFailHistoryRepository, IUnitOfWork unitOfWork)
		{
			_accountLoginFailHistoryRepository = accountLoginFailHistoryRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<int> AddAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginFailHistory));

			await _accountLoginFailHistoryRepository.AddAsync(accountLoginFailHistory);
			return await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginFailHistory));

			_accountLoginFailHistoryRepository.Delete(accountLoginFailHistory);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));

			var accountLoginFailHistory = await _accountLoginFailHistoryRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			if (accountLoginFailHistory is null)
				throw new AretechException("Silinecek veri bulunamadı.");

			return await DeleteAsync(accountLoginFailHistory);
		}

		public async Task<List<AccountLoginFailHistory>> GetAccountsAsync(CancellationToken cancellationToken = default)
		{
			var accountLoginFailHistories = await _accountLoginFailHistoryRepository.TableNoTracking.ToListAsync();
			return accountLoginFailHistories;
		}

		public async Task<bool> UpdateAsync(AccountLoginFailHistory accountLoginFailHistory, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(accountLoginFailHistory));

			_accountLoginFailHistoryRepository.Update(accountLoginFailHistory);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));

			var accountLoginFailHistory = await _accountLoginFailHistoryRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			return await UpdateAsync(accountLoginFailHistory);
		}
	}
}
