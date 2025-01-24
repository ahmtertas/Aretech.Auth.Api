using Aretech.Core;
using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers;
using Aretech.Services.Accounts.Models;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Services.Accounts.AccountsService
{
	public class AccountService : IAccountService
	{
		private readonly IRepository<Account> _accountRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHashService _hashService;

		public AccountService
			(
						 IRepository<Account> accountRepository,
						 IUnitOfWork unitOfWork,
						 IHashService hashService
			)
		{
			_accountRepository = accountRepository;
			_unitOfWork = unitOfWork;
			_hashService = hashService;
		}
		public async Task<int> AddAsync(Account account, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(account);
			account.SetCreatedBy(Guid.Parse("8bb39c8d-e8c6-4221-9421-f24de0167ba4"));
			account.PasswordHash = _hashService.HashPassword(account.PasswordHash);
			await _accountRepository.AddAsync(account);
			return await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(Account account, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(account);

			_accountRepository.Delete(account);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(id);

			var account = await _accountRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			if (account is null)
				throw new AretechException("Silinecek hesap bulunamadı.");

			return await DeleteAsync(account);
		}

		public async Task<List<Account>> GetAccountsAsync(CancellationToken cancellationToken = default)
		{
			var accounts = await _accountRepository.TableNoTracking.ToListAsync();
			return accounts;
		}

		public async Task<bool> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(loginModel);

			if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
				throw new AretechException("Kullanıcı adı ve/veya şifre eksik.");

			var user = await _accountRepository.TableNoTracking.Where(x => x.Username == loginModel.UserName).FirstOrDefaultAsync();
			if (user is null)
				throw new AretechException("Kullanıcı bulunamadı.");

			if (!_hashService.VerifyPassword(loginModel.Password, user.PasswordHash))
				throw new AretechException("Kullanıcı adı ve/veya şifre eksik.");

			return true;
		}

		public async Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(account));

			_accountRepository.Update(account);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateAsync(Guid id, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));

			var account = await _accountRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
			return await UpdateAsync(account, cancellationToken);
		}
	}
}
