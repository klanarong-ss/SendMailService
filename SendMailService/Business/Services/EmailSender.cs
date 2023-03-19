using EmailService.SendMailService.DataAccess.Models;
using SendMailService.Business.InterfaceServices;
using SendMailService.DataAccess.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SendMailService.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
            //emailMessage.To.AddRange(message.To);
            emailMessage.To.Add(MailboxAddress.Parse(message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch(Exception ex)
                {
                    //log an error message or throw an exception or both.
                    throw new Exception(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

    }
}
