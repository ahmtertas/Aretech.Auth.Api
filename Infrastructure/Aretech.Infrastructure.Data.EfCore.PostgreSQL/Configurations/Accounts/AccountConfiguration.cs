using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class AccountConfiguration : AretechGuidEntityConfiguration<Account>
	{
		public override void Configure(EntityTypeBuilder<Account> builder)
		{

			builder.HasKey(x => x.Id).HasName("Account_pkey");
			builder.ToTable("Account");

			base.Configure(builder);
		}
	}
}