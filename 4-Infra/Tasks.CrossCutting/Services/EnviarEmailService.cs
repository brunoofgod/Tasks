using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tasks.CrossCutting.Services
{
    public class EnviarEmailService
    {

        public void Execute(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(email, "Brunoofgod")
                };

                mail.To.Add(new MailAddress(email));

                mail.Subject = "Recuperação de senha";
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient("smtp.prismafive.com.br", 587))
                {
                    smtp.Credentials = new NetworkCredential("suporte@prismafive.com.br", "");
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
