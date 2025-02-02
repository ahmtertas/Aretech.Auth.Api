using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;
using System.Collections.ObjectModel;

namespace Aretech.Domain.Accounts
{
	public class Account : Entity, IAggregateRoot
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; } = null!;
		public string? IdentityNumber { get; set; } = null!;
		public string? FirstName { get; set; } = null!;
		public string? LastName { get; set; } = null!;
		public string? RefreshToken { get; set; } = null!;
		public DateTime? RefreshTokenExpiryTime { get; set; } = null!;

		public bool IsActived { get; set; } = true;
		public bool IsVerified { get; set; } = false;
		public bool TwoFactorEnabled { get; set; } = false;
		public DateTime? LockoutEnd { get; set; }
		public DateTime? LastLogin { get; set; }
		public int FailedLoginAttempts { get; set; } = 0!;


		public Collection<AccountLoginFailHistory> AccountLoginFailHistorys { get; set; } = new Collection<AccountLoginFailHistory>();
		public Collection<AccountLoginHistory> AccountLoginHistorys { get; set; } = new Collection<AccountLoginHistory>();
		public Collection<BlacklistedToken> BlacklistedTokens { get; set; } = new Collection<BlacklistedToken>();
		public Collection<PasswordReset> PasswordResets { get; set; } = new Collection<PasswordReset>();
	}
}