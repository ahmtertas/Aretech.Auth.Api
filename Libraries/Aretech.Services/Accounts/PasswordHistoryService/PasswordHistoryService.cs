
using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;

namespace Aretech.Services.Accounts.PasswordHistoryService
{
	public class PasswordHistoryService : IPasswordHistoryService
	{
		private readonly IRepository<PasswordHistory> _repository;
		private readonly IUnitOfWork _unitOfWork;
		public PasswordHistoryService
			(
						 IRepository<PasswordHistory> repository,
						 IUnitOfWork unitOfWork
			)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<int> AddAsync(PasswordHistory passwordHistory)
		{
			ArgumentNullException.ThrowIfNull(passwordHistory);

			await _repository.AddAsync(passwordHistory);
			return await _unitOfWork.SaveChangesAsync();
		}
	}
}