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

			builder.Property(x => x.CreatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.UpdatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.DeletedDate).HasColumnType("timestamp without time zone");

			base.Configure(builder);
		}
	}
}