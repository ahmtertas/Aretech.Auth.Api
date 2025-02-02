using Aretech.Services.Accounts.AccountLoginFailHistoryService;
using Aretech.Services.Accounts.AccountLoginHistoryService;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.BlackListedTokenService;
using Aretech.Services.Accounts.PasswordHistoryService;
using Aretech.Services.Accounts.PasswordResetService;
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
			services.AddScoped<IBlackListedTokenService, BlackListedTokenService>();
			services.AddScoped<IPasswordResetService, PasswordResetService>();
			services.AddScoped<IPasswordHistoryService, PasswordHistoryService>();

			return services;
		}

	}
}