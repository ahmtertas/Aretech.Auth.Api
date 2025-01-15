using Aretech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract
{
	public class AretechIntSoftDeleteEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseIntSoftDeleteEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasQueryFilter(x => !x.IsDeleted);
		}
	}
}