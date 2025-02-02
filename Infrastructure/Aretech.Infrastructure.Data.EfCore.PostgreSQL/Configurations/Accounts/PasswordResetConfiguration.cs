using Aretech.Domain.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts
{
	public class PasswordResetConfiguration : AretechGuidEntityConfiguration<PasswordReset>
	{
		public override void Configure(EntityTypeBuilder<PasswordReset> builder)
		{

			builder.HasKey(x => x.Id).HasName("PasswordResetRequest_pkey");
			builder.ToTable("PasswordResetRequest");

			base.Configure(builder);
		}
	}
}