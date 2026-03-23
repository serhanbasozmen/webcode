using System.Net;
using System.Net.Mail;

namespace dotnet_store.Models;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}


public class SmtpEmailService : IEmailService
{
    private IConfiguration _configurtaion;
    public SmtpEmailService(IConfiguration configurtaion)
    {
        _configurtaion = configurtaion;
    }
    public async Task SendEmailAsync(string email, string subject, string message)
    {

        using (var client = new SmtpClient(_configurtaion["Email:Host"]))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configurtaion["Email:Username"], _configurtaion["Email:Password"]);

            client.Port = 587;
            client.EnableSsl = true;
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configurtaion["Email:Username"]!),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            await client.SendMailAsync(mailMessage);
        }
    }
}

