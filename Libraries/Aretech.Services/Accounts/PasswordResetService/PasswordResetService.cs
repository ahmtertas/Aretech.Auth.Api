using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Services.Accounts.PasswordResetService
{
	public class PasswordResetService : IPasswordResetService
	{
		private readonly IRepository<PasswordReset> _repository;
		private readonly IUnitOfWork _unitOfWork;
		public PasswordResetService
			(
						 IRepository<PasswordReset> repository,
						 IUnitOfWork unitOfWork
			)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}
		public async Task<string> AddPasswordResetAsync()
		{
			var token = Guid.NewGuid().ToString();
			var expiry = DateTime.Now.AddMinutes(2); // 2 dakika geçerli

			var passwordReset = new PasswordReset
			{
				Token = token,
				Expiration = expiry
			};
			passwordReset.SetCreatedBy(Guid.NewGuid());
			await _repository.AddAsync(passwordReset);
			await _unitOfWork.SaveChangesAsync();

			return token;
		}

		public async Task<bool> ValidatePasswordResetToken(string token, DateTime expiry)
		{
			return await _repository.TableNoTracking
									.AnyAsync(x => x.Token.Equals(token)
												&& x.Expiration >= DateTime.Now);
		}
	}
}