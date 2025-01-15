using Aretech.Services.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Services
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IAccountService, AccountService>();

			return services;
		}

	}
}