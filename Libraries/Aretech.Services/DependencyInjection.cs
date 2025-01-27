using Aretech.Services.Accounts.AccountLoginFailHistoryService;
using Aretech.Services.Accounts.AccountLoginHistoryService;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.SeedWorks.DeviceInfoService;
using Aretech.Services.SeedWorks.Security;
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
			services.AddScoped<ITokenService, JwtTokenService>();
			services.AddScoped<IDeviceInfoService, DeviceInfoService>();


			return services;
		}

	}
}