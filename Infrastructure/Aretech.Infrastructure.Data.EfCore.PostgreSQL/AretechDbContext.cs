using Aretech.Domain.Accounts;
using Aretech.Domain.Applications;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Configurations.Accounts;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL
{
	public class AretechDbContext : DbContext
	{
		public AretechDbContext(DbContextOptions<AretechDbContext> options) : base(options)
		{
		}

		#region Entities

		DbSet<Account> Accounts { get; set; }
		DbSet<AccountLoginFailHistory> AccountLoginFailHistories { get; set; }
		DbSet<AccountLoginHistory> AccountLoginHistories { get; set; }
		DbSet<Application> Applications { get; set; }
		DbSet<AccountApplicationMapping> AccountApplicationMappings { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDbFunction(() => ToTurkishLower(default)).HasName("ToTurkishLower");
			modelBuilder.HasDbFunction(() => ToTurkishUpper(default)).HasName("ToTurkishUpper");

			ApplyConfiguration(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		private void ApplyConfiguration(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AccountConfiguration());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(new EntitySaveChangesInterceptor());
			base.OnConfiguring(optionsBuilder);
		}

		[DbFunction]
		public static string ToTurkishLower(string input) => input.ToLower(new System.Globalization.CultureInfo("tr-TR"));

		[DbFunction]
		public static string ToTurkishUpper(string input) => input.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
	}
}