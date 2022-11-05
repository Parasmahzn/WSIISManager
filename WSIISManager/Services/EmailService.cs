using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace WSIISManager.Services;

public class EmailService : IEmailService
{
    public Task SendEmail(Email email)
    {
        EmailRequest recipient = new() { EmailSubject = "IIS Stopped: Error Occured", EmailTo = email.Destination };
        recipient.EmailBody = GetErrorDetailsFromEventViewer();
        var emailResp = IsEmailSent(recipient, email);
        if (emailResp)
            return Task.CompletedTask;
        else
            //Perform something else in future for unsuccesful email send
            return Task.CompletedTask;
    }
    /// <summary>
    /// Sending an email
    /// Password encryption and decryption yet to achieve
    /// </summary>
    /// <param name="recipient"></param>
    /// <param name="sender"></param>
    /// <returns></returns>
    private bool IsEmailSent(EmailRequest recipient, Email sender)
    {
        bool sent = false;
        try
        {
            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse(sender.Source.Username));
            email.To.Add(MailboxAddress.Parse(recipient.EmailTo));
            email.Subject = recipient.EmailSubject;
            email.Body = new TextPart(TextFormat.Html) { Text = recipient.EmailBody };


            using (var smtp = new SmtpClient())
            {
                smtp.Connect(sender.Source.Host, sender.Source.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(sender.Source.Username, sender.Source.Password); // TO DO Secure the password 
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            sent = true;
        }
        catch (Exception)
        {
            // Do something for Exception//
            //Log Data//
        }
        return sent;
    }

    private string GetErrorDetailsFromEventViewer()
    {
        //Get Application Error Details from Event Viewer using 
        return string.Empty;
    }
}
