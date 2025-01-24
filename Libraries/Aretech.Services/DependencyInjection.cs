using Aretech.Services.Accounts.AccountLoginFailHistoryService;
using Aretech.Services.Accounts.AccountLoginHistoryService;
using Aretech.Services.Accounts.AccountsService;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Services
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IAccountLoginHistoryService, AccountLoginHistoryService>();
			services.AddScoped<IAccountLoginFailHistoryService, AccountLoginFailHistoryService>();

			return services;
		}

	}
}