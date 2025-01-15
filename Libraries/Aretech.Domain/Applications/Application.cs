using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;

namespace Aretech.Domain.Applications
{
	public class Application : Entity, IAggregateRoot
	{
		public string Name { get; set; } = null!;
	}
}