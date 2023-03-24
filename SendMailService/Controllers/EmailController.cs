using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace SendMailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public EmailController()
        {

        }

        [HttpGet]
        [Route("Test-1")]
        public IActionResult Test1()
        {
            return Ok("Test-01");
        }

        [HttpPost]
        [Route("Test-2")]
        public IActionResult Test2()
        {
            return Ok("Test-02");
        }

        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("klanarong717@gmail.com"));
            email.To.Add(MailboxAddress.Parse("klanarong717@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("klanarong717@gmail.com", "buqisviurlclkbqf");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            return Ok();
        }
    }
}
