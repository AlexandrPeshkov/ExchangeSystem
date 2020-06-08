using System.Net;
using System.Net.Mail;

namespace ES.Infrastructure.Services
{
    public class EmailService
    {
        private const string fromEmail = "peshkov.alexandr.net@gmail.com";
        private const string password = "alexjey38";

        private const string subject = "EchagneSystem  - Торговый сигнал";
        public void Send(string email, string data)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = data;
                mail.IsBodyHtml = false;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(fromEmail, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

            }
        }
    }
}
