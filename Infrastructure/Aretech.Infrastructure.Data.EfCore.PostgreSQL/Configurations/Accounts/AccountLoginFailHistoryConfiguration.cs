using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class AccountLoginFailHistoryConfiguration : AretechGuidEntityConfiguration<AccountLoginFailHistory>
	{
		public override void Configure(EntityTypeBuilder<AccountLoginFailHistory> builder)
		{

			builder.HasKey(x => x.Id).HasName("AccountLoginFailHistory_pkey");
			builder.ToTable("AccountLoginFailHistory");

			builder.Property(x => x.CreatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.UpdatedDate).HasColumnType("timestamp without time zone");
			builder.Property(x => x.DeletedDate).HasColumnType("timestamp without time zone");

			base.Configure(builder);
		}
	}
}