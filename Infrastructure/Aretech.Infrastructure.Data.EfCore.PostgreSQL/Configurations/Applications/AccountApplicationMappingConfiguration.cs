using Aretech.Domain.Applications;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Applications
{
	public class AccountApplicationMappingConfiguration : AretechGuidEntityConfiguration<AccountApplicationMapping>
	{
		public override void Configure(EntityTypeBuilder<AccountApplicationMapping> builder)
		{

			builder.HasKey(x => x.Id).HasName("AccountApplicationMapping_pkey");
			builder.ToTable("AccountApplicationMapping");

			builder.Property(x => x.CreatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.UpdatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.DeletedDate).HasColumnType("timestamp without time zone");

			base.Configure(builder);
		}
	}
}