using System.Net;
using System.Net.Mail;
using BookManagement.Application.Common.Settings;
using BookManagement.Application.Notifications.Brokers;
using BookManagement.Application.Notifications.Models;
using Microsoft.Extensions.Options;

namespace BookManagement.Infrastructure.Notifications.Brokers;

public class SmtpEmailSenderBroker : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _smtpEmailSenderSettings;

    public SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> smtpEmailSenderSettings)
    {
        _smtpEmailSenderSettings = smtpEmailSenderSettings.Value;
    }

    public ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var mail = new MailMessage(emailMessage.SenderEmailAddress, emailMessage.ReceiverEmailAddress);
        mail.Subject = emailMessage.Subject;
        mail.Body = emailMessage.Body;
        mail.IsBodyHtml = true;

        var smtpClient = new SmtpClient(_smtpEmailSenderSettings.Host, _smtpEmailSenderSettings.Port);
        smtpClient.Credentials = new NetworkCredential(_smtpEmailSenderSettings.CredentialAddress, _smtpEmailSenderSettings.Password);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);

        return new ValueTask<bool>(true);
    }
}