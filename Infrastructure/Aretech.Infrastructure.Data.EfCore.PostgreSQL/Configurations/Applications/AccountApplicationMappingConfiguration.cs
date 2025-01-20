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

			base.Configure(builder);
		}
	}
}