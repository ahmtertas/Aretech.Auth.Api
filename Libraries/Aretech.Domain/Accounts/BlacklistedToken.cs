using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;

namespace Aretech.Domain.Accounts
{
	public class BlacklistedToken : Entity, IAggregateRoot
	{
		public string Token { get; set; } = default!;

		public Guid AccountId { get; set; }

		public DateTime ExpiryDate { get; set; }


		public virtual Account Account { get; set; }
	}
}