using EmailService.SendMailService.DataAccess.Models;

namespace SendMailService.Business.InterfaceServices
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
