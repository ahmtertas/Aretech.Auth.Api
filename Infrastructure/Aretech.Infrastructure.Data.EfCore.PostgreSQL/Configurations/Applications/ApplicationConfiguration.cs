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

			builder.Property(x => x.CreatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.UpdatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.DeletedDate).HasColumnType("timestamp without time zone");

			base.Configure(builder);
		}
	}
}