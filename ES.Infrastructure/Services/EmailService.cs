using System.Net;
using System.Net.Mail;

namespace ES.Infrastructure.Services
{
    public class EmailService
    {
        public void Send(string email, string data)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("peshkov.alexandr.net@gmail.com", "mypwd"),
                EnableSsl = true
            };
            client.Send("peshkov.alexandr.net@gmail.com", email, "EchagneSystem Trading Signals", data);
        }
    }
}
