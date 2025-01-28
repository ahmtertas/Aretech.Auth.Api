using Aretech.Core;
using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers;
using Aretech.Services.Accounts.AccountLoginFailHistoryService;
using Aretech.Services.Accounts.AccountLoginHistoryService;
using Aretech.Services.Accounts.Models;
using Aretech.Services.SeedWorks.DeviceInfoService;
using Aretech.Services.SeedWorks.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aretech.Services.Accounts.AccountsService
{
	public class AccountService : IAccountService
	{
		private readonly IRepository<Account> _accountRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHashService _hashService;
		private readonly ITokenService _tokenService;
		private readonly IAccountLoginHistoryService _accountLoginHistoryService;
		private readonly IAccountLoginFailHistoryService _accountLoginFailHistoryService;
		private readonly IConfiguration _configuration;
		private readonly IDeviceInfoService _deviceInfoService;

		public AccountService
		(
						 IRepository<Account> accountRepository,
						 IUnitOfWork unitOfWork,
						 IHashService hashService,
						 ITokenService tokenService,
						 IAccountLoginHistoryService accountLoginHistoryService,
						 IAccountLoginFailHistoryService accountLoginFailHistoryService,
						 IConfiguration configuration,
						 IDeviceInfoService deviceInfoService
		)
		{
			_accountRepository = accountRepository;
			_unitOfWork = unitOfWork;
			_hashService = hashService;
			_tokenService = tokenService;
			_accountLoginHistoryService = accountLoginHistoryService;
			_accountLoginFailHistoryService = accountLoginFailHistoryService;
			_configuration = configuration;
			_deviceInfoService = deviceInfoService;
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

		public async Task<Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellation = default)
		{
			ArgumentNullException.ThrowIfNull(userName);

			var account = await _accountRepository.Table.FirstOrDefaultAsync(x => x.Username.Equals(userName));
			return account;
		}

		public async Task<List<Account>> GetAccountsAsync(CancellationToken cancellationToken = default)
		{
			var accounts = await _accountRepository.TableNoTracking.ToListAsync();
			return accounts;
		}

		public async Task<string> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(loginModel);

			string token = String.Empty;
			var osName = _deviceInfoService.GetOsName();
			var (ipv4, ipv6) = _deviceInfoService.GetIpAddresses();
			var macAddress = _deviceInfoService.GetMacAddress();

			if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
				throw new AretechException("Kullanıcı adı ve/veya şifre eksik.", System.Net.HttpStatusCode.Unauthorized);

			var user = await _accountRepository.TableNoTracking.Where(x => x.Username == loginModel.UserName).FirstOrDefaultAsync();
			if (user is null)
				throw new AretechException("Kullanıcı bulunamadı.", System.Net.HttpStatusCode.Unauthorized);

			if (!_hashService.VerifyPassword(loginModel.Password, user.PasswordHash))
			{
				var accountLoginFailHistory = new AccountLoginFailHistory
				{
					AccountId = user.Id,
					OsName = osName,
					Ipv6Address = ipv6,
					Ipv4Address = ipv4,
					MacAddress = macAddress,

				};
				accountLoginFailHistory.SetCreatedBy(user.Id);
				await _accountLoginFailHistoryService.AddAsync(accountLoginFailHistory);

				throw new AretechException("Kullanıcı adı ve/veya şifre eksik.", System.Net.HttpStatusCode.Unauthorized);
			}
			else
			{
				token = _tokenService.GenerateToken(user);
				var accountLoginHistory = new AccountLoginHistory
				{
					AccountId = user.Id,
					AccessToken = token,
					ExpiredIn = Convert.ToInt32(_configuration["ExpiresInMinutes"]),
					RefreshToken = token,
					OsName = osName,
					Ipv6Address = ipv6,
					Ipv4Address = ipv4,
					MacAddress = macAddress,
				};
				accountLoginHistory.SetCreatedBy(user.Id);
				await _accountLoginHistoryService.AddAsync(accountLoginHistory);
			}

			return token;
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
