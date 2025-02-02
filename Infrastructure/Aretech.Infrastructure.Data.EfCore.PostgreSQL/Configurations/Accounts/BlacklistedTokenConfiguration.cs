using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class BlacklistedTokenConfiguration : AretechGuidEntityConfiguration<BlacklistedToken>
	{
		public override void Configure(EntityTypeBuilder<BlacklistedToken> builder)
		{

			builder.HasKey(x => x.Id).HasName("BlacklistedToken_pkey");
			builder.ToTable("BlacklistedToken");

			base.Configure(builder);
		}
	}
}
