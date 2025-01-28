using Aretech.Infrastructure.Abstract;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aretech.Infrastructure.Concrete
{
	public class SendGridEmailService : IEmailService
	{
		private readonly string _sendGridApiKey;

		public SendGridEmailService(string sendGridApiKey)
		{
			_sendGridApiKey = sendGridApiKey;
		}

		public async Task SendEmailAsync(string toEmail, string subject, string content)
		{
			var client = new SendGridClient(_sendGridApiKey);
			var from = new EmailAddress("noreply@yourdomain.com", "Your App Name");
			var to = new EmailAddress(toEmail);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
			var response = await client.SendEmailAsync(msg);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("E-posta gönderimi başarısız oldu.");
			}
		}
	}
}
