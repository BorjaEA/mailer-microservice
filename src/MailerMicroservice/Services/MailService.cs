// src/MailerMicroservice/Services/MailService.cs
using MailerMicroservice.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailerMicroservice.Services
{
    public class MailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMailAsync(MailRequest request)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Mailer", _config["Smtp:From"]));
            message.To.Add(new MailboxAddress("", request.To));
            message.Subject = request.Subject;
            message.Body = new TextPart("plain") { Text = request.Body };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["Smtp:Host"],
                int.Parse(_config["Smtp:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls
            );
            await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
