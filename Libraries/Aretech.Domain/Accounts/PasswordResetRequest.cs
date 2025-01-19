using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;

namespace Aretech.Domain.Accounts
{
	public class PasswordResetRequest : Entity, IAggregateRoot
	{
		public Guid AccountId { get; set; }
		public string Token { get; set; } = null!;
		public DateTime Expiration { get; set; }
		public bool IsUsed { get; set; }


		public Account Account { get; set; }
	}
}