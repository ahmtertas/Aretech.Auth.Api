using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class PasswordHistoryConfiguration : AretechGuidEntityConfiguration<PasswordHistory>
	{
		public override void Configure(EntityTypeBuilder<PasswordHistory> builder)
		{

			builder.HasKey(x => x.Id).HasName("PasswordHistory_pkey");
			builder.ToTable("PasswordHistory");

			builder.Property(x => x.CreatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.UpdatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.DeletedDate).HasColumnType("timestamp without time zone");

			base.Configure(builder);
		}
	}
}