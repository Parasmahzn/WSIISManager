using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Diagnostics;
using System.Text;
using WSIISManager.Library;

namespace WSIISManager.Services;

public class EmailService : IEmailService
{
    public Task SendEmail(Email email)
    {
        EmailRequest recipient = new() { EmailSubject = "IIS Stopped: Error Occured", EmailTo = email.Destination };
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
            email.Body = GetErrorDetailsFromEventViewer();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(sender.Source.Host, sender.Source.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(sender.Source.Username, sender.Source.Password); // TO DO Secure the password 
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            sent = true;
        }
        catch (Exception ex)
        {
            // Do something for Exception//
            //Log Data//
        }
        return sent;
    }

    private TextPart GetErrorDetailsFromEventViewer()
    {
        var emailContent = new TextPart(TextFormat.Html);
        try
        {
#pragma warning disable CA1416 // Validate platform compatibility
            EventLog eventLog = new("Application");
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
            var entries = eventLog.Entries.Cast<EventLogEntry>()
                          .Where(x => x.TimeGenerated >= DateTime.Today && x.EntryType == EventLogEntryType.Error)
                          .Select(x => new
                          {
                              x.MachineName,
                              x.Site,
                              x.Source,
                              x.Message
                          }).ToList();
#pragma warning restore CA1416 // Validate platform compatibility
            if (entries.Count > 0)
            {
                var sb = new StringBuilder();
                sb.Append("<body><p>Dear System Admin,</p> <p>Following are the list of exception occured in the IIS System." +
                         "</br> Please make note of it and perform necessary actions</p>");
                sb.Append("<ol>");
                foreach (var item in entries)
                {
                    sb.Append("<li>" + item + "</li>");
                }
                sb.Append("<p>Sincerely,<br>-Developer</br></p> </body>");
                emailContent.Text = sb.ToString();
            }
        }
        catch (Exception)
        {
            emailContent.Text = string.Empty;
        }
        return emailContent;
    }

    private string DecryptParameter(string encryptedData)
    {
        StringCypher cypher = new("fklasjfkasdjfkasjfkjhahsdjkdf");
        return cypher.Decrypt(encryptedData);
    }
}
