
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Http;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
namespace E_Commerce.Email_Seneding
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {
        //public async Task SendEmailAsync(string subject , string toEmail , string username , string message)
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("kmoamed942@gmail.com", "E-CommerceApp");
        //    //var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress(toEmail, username);
        //    var plainTextContent = message;
        //    var htmlContent = "";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
        public async Task SendEmailAsync(string subject, string toEmail, string message)
        {
            var email = new MimeMessage();

            var sender = _configuration["EmailData:Email"];
            var appPassword = _configuration["EmailData:Password"];
            var host = _configuration["EmailData:Host"];
            var port = _configuration["EmailData:Port"];

            email.From.Add(MailboxAddress.Parse(sender));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = message };

            using var smtp = new SmtpClient();

            smtp.Connect(host, Convert.ToInt32(port), SecureSocketOptions.StartTls);
            smtp.Authenticate(sender, appPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
