using Aretech.Infrastructure.Abstract;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Aretech.Infrastructure.Concrete
{
	public class SendGmailEmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public SendGmailEmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			try
			{
				var email = _configuration["EmailConfiguration:Email"];
				var password = _configuration["EmailConfiguration:Password"];
				var port = Convert.ToInt32(_configuration["EmailConfiguration:Port"]);
				var host = _configuration["EmailConfiguration:Host"];

				var smtpClient = new SmtpClient("smtp.gmail.com")
				{
					Port = 587,  // TLS için 587, SSL için 465 kullanılır
					Credentials = new NetworkCredential(email, password),
					EnableSsl = true, // SSL/TLS bağlantısını etkinleştir
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(email),
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				};
				mailMessage.To.Add(toEmail);
				await smtpClient.SendMailAsync(mailMessage);
			}
			catch (Exception ex) { }
		}
	}
}