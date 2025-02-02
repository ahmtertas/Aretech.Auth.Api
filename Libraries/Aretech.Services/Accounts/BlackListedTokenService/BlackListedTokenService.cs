using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Services.Accounts.BlackListedTokenService
{
	public class BlackListedTokenService : IBlackListedTokenService
	{
		private readonly IRepository<BlacklistedToken> _repository;
		private readonly IUnitOfWork _unitOfWork;
		public BlackListedTokenService
			(
						IRepository<BlacklistedToken> repository,
						IUnitOfWork unitOfWork
			)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<int> AddAsync(BlacklistedToken blacklistedToken, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(blacklistedToken);

			await _repository.AddAsync(blacklistedToken, cancellationToken);
			return await _unitOfWork.SaveChangesAsync();
		}

		public async Task<BlacklistedToken?> GetBlacklistedTokenByTokenAndAccountIdAsync(string? token, Guid accountId, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(accountId);

			var blacklistedToken = await _repository.TableNoTracking.FirstOrDefaultAsync(x => x.Token == token && x.AccountId == accountId);
			return blacklistedToken;
		}
	}
}