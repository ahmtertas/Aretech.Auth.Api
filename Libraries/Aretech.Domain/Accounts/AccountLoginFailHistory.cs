using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;

namespace Aretech.Domain.Accounts
{
	public class AccountLoginFailHistory : Entity, IAggregateRoot
	{
		public Guid AccountId { get; set; }

		public string? ClientName { get; set; } = null!;
		public string? ClientVersion { get; set; } = null!;
		public string? Ipv4Address { get; set; }
		public string? Ipv6Address { get; set; }
		public string? MacAddress { get; set; }
		public string? Scopes { get; set; }
		public string? OsName { get; set; }

		public virtual Account Account { get; set; }
	}
}
