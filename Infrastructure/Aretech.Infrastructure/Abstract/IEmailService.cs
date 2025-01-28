namespace Aretech.Infrastructure.Abstract
{
	public interface IEmailService
	{
		Task SendEmailAsync(string toEmail, string subject, string content);
	}
}
