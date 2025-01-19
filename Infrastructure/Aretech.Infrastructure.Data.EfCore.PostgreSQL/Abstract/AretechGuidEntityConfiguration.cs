using Aretech.SharedKernel;
using Aretech.SharedKernel.SeedWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract
{
	public abstract class AretechGuidEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity, IAggregateRoot
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
		}
	}
}