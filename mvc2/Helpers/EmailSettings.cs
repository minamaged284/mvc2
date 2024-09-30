using dal.Model;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace mvc2.Helpers
{
	public class EmailSettings
	{
		private readonly MailSettings  _options;

		public EmailSettings(IOptions<MailSettings> options)
        {
			_options = options.Value;
		}

        public void SendEmail(Email email)
		{
			//var client = new SmtpClient("smtp.gmail.com", 587);
			//client.UseDefaultCredentials = false;
			//client.EnableSsl = true;
			//client.Credentials = new NetworkCredential("minamina284@gmail.com", "dzpv miqw bfqs cypw");
			//client.Send("minamina284@gmail.com", email.Recipients, email.Subject, email.Body);

			var mail = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_options.Email),
				Subject = email.Subject,
			};
			mail.To.Add(MailboxAddress.Parse(email.Recipients));
			var builder = new BodyBuilder();
			builder.TextBody = email.Body;
			mail.Body=builder.ToMessageBody();
			mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));
			using var smtp = new SmtpClient();
			smtp.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(_options.Email,_options.Password);
			smtp.Send(mail);
			smtp.Disconnect(true);
		}
	}
}
