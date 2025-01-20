using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class RefreshTokenConfiguration : AretechGuidEntityConfiguration<RefreshToken>
	{
		public override void Configure(EntityTypeBuilder<RefreshToken> builder)
		{

			builder.HasKey(x => x.Id).HasName("RefreshToken_pkey");
			builder.ToTable("RefreshToken");

			base.Configure(builder);
		}
	}
}