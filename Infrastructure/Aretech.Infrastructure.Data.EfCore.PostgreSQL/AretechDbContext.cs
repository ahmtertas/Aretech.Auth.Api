using Aretech.Domain.Accounts;
using Aretech.Domain.Applications;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Applications;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL
{
	public class AretechDbContext : DbContext
	{
		private readonly IHashService _hashService;
		public AretechDbContext(DbContextOptions<AretechDbContext> options, IHashService hashService) : base(options)
		{
			_hashService = hashService;
		}

		#region Entities

		DbSet<Account> Accounts { get; set; }
		DbSet<AccountLoginFailHistory> AccountLoginFailHistories { get; set; }
		DbSet<AccountLoginHistory> AccountLoginHistories { get; set; }
		DbSet<Application> Applications { get; set; }
		DbSet<AccountApplicationMapping> AccountApplicationMappings { get; set; }
		DbSet<PasswordHistory> PasswordHistories { get; set; }
		DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
		DbSet<RefreshToken> RefreshTokens { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDbFunction(() => ToTurkishLower(default)).HasName("ToTurkishLower");
			modelBuilder.HasDbFunction(() => ToTurkishUpper(default)).HasName("ToTurkishUpper");

			var hashedPassword = _hashService.HashPassword("~t}k+6Bp0MtH!v!");

			modelBuilder.Entity<Account>().HasData(
						   new Account
						   {
							   Id = Guid.NewGuid(),
							   IdentityNumber = string.Empty,
							   IsActived = true,
							   IsVerified = false,
							   FirstName = "Süper",
							   LastName = "Admin",
							   Username = "superadmin",
							   Email = "ar3t3ch@gmail.com",
							   PhoneNumber = "5452158345",
							   PasswordHash = hashedPassword,
						   });

			ApplyConfiguration(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		private void ApplyConfiguration(ModelBuilder modelBuilder)
		{
			#region Account
			modelBuilder.ApplyConfiguration(new AccountConfiguration());
			modelBuilder.ApplyConfiguration(new AccountLoginHistoryConfiguration());
			modelBuilder.ApplyConfiguration(new AccountLoginFailHistoryConfiguration());
			modelBuilder.ApplyConfiguration(new PasswordHistoryConfiguration());
			modelBuilder.ApplyConfiguration(new PasswordResetRequestConfiguration());
			modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
			#endregion

			#region Application
			modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
			modelBuilder.ApplyConfiguration(new AccountApplicationMappingConfiguration());
			#endregion
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(new EntitySaveChangesInterceptor());

			optionsBuilder.ConfigureWarnings(warnings =>
		   warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

			base.OnConfiguring(optionsBuilder);
		}

		[DbFunction]
		public static string ToTurkishLower(string input) => input.ToLower(new System.Globalization.CultureInfo("tr-TR"));

		[DbFunction]
		public static string ToTurkishUpper(string input) => input.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
	}
}