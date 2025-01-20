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

			base.Configure(builder);
		}
	}
}