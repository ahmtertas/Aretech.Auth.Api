using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class AccountLoginHistoryConfiguration : AretechGuidEntityConfiguration<AccountLoginHistory>
	{
		public override void Configure(EntityTypeBuilder<AccountLoginHistory> builder)
		{

			builder.HasKey(x => x.Id).HasName("AccountLoginHistory_pkey");
			builder.ToTable("AccountLoginHistory");

			base.Configure(builder);
		}
	}
}