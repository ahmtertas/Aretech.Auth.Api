using Aretech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract
{
	public abstract class AretechIntEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseIntEntity
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}