using Aretech.Domain.Accounts;
using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;


namespace Aretech.Domain.Applications
{
	public class AccountApplicationMapping : Entity, IAggregateRoot
	{
		public Guid AccountId { get; set; }
		public Guid ApplicationId { get; set; }


		public virtual Account Account { get; set; }
		public virtual Application Application { get; set; }
	}
}
