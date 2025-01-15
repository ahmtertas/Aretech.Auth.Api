using Aretech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract
{
	public abstract class AretechGuidEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseGuidEntity
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}