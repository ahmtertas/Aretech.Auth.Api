using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;

namespace Aretech.Domain.Accounts
{
	public class PasswordHistory : Entity, IAggregateRoot
	{
		public Guid AccountId { get; set; }
		public string OldPasswordHash { get; set; } = null!;


		public Account Account { get; set; }
	}
}
