using Aretech.Domain.Applications;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Applications
{
	public class ApplicationConfiguration : AretechGuidEntityConfiguration<Application>
	{
		public override void Configure(EntityTypeBuilder<Application> builder)
		{

			builder.HasKey(x => x.Id).HasName("Application_pkey");
			builder.ToTable("Application");

			base.Configure(builder);
		}
	}
}