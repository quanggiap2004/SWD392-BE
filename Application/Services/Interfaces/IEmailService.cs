namespace Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendPaymentConfirmationEmailAsync(string email, string username, int orderId);
        Task SendRegistrationConfirmationEmailAsync(string email, string confirmationLink, string fullName);

    }
}
