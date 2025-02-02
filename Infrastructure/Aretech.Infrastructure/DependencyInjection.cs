using Aretech.Infrastructure.Abstract;
using Aretech.Infrastructure.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();

			var sendGridApiKey = configuration["SendGrid:ApiKey"];

			services.AddScoped<IEmailService, SendGmailEmailService>();

			return services;
		}
	}
}