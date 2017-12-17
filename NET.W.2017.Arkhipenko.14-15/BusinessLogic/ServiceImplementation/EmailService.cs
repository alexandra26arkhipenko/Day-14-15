using System;
using System.Net;
using System.Net.Mail;
using BusinessLogic.Interfaces.Interfaces;

namespace BusinessLogic.ServiceImplementation
{
    public class EmailService : IEmailService
    {
        public void SendMail(MailData mailData)
        {
            var data = ConfigureData(mailData);

            var email = data.Item1;
            var smtp = data.Item2;

            smtp.Send(email);
        }

        
        private static Tuple<MailMessage, SmtpClient> ConfigureData(MailData mailData)
        {
            var from = new MailAddress(mailData.From);
            var to = new MailAddress(mailData.To);

            var email = new MailMessage(from, to)
            {
                Subject = mailData.Subject,
                Body = mailData.Message,
                IsBodyHtml = true
            };

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Credentials = new NetworkCredential(mailData.From, mailData.FromPassword),
                EnableSsl = true
            };

            return Tuple.Create(email, smtp);
        }
    }
}