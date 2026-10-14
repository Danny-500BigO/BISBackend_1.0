using BakeryApi.Application.Services.Email;
using BakeryApi.Domain.Entities;
using Google.Protobuf;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Encodings;


namespace BakeryApi.Application.Services
{


    class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailConfirmationAsync(string token, string email)
        {

            var host = _configuration["EmailSettings:Host"] ?? throw new InvalidOperationException("Email Host is Missing");
            var port = int.Parse(
                _configuration["EmailSettings:Port"] ?? throw new InvalidOperationException("Email port is missing"));

            var username =
                _configuration["EmailSettings:Username"] ?? throw new InvalidOperationException("Email username is missing");

            var password =
                _configuration["EmailSettings:AppPassword"] ?? throw new InvalidOperationException("Email app password is missing");


                var resetLink =
            $"http://localhost:4200/reset-password?token={Uri.EscapeDataString(token)}";


            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("BakeryAPI", username));
            message.To.Add(MailboxAddress.Parse(email) );
            message.Subject = "Reset Your Password" ;

            message.Body = new TextPart("plain")
            {
            Text = $"""
                Hello,

                We received a request to reset your password.

                Please open this link to choose a new password:
                {resetLink}

                This link expires in 15 minutes. If you did not request
                a password reset, you can ignore this email.
                """
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                host,
                port,
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(username,password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return;
        }

    }

}

