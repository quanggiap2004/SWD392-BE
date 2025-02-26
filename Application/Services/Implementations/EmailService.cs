using Application.Services.Interfaces;
using Common.Helper;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:From"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendPaymentConfirmationEmailAsync(string email, string fullName, int orderId)
        {
            string subject = "Payment Confirmation";
            string message = EmailContentBuilder.BuildPaymentConfirmationEmail(fullName, orderId);
            await SendEmailAsync(email, subject, message);
        }

        public async Task SendRegistrationConfirmationEmailAsync(string email, string confirmationLink, string fullName)
        {
            string subject = "Confirm your email";
            string message = EmailContentBuilder.BuildRegistrationConfirmationEmail(confirmationLink, fullName);
            await SendEmailAsync(email, subject, message);
        }
    }
}
