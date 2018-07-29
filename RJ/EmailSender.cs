
using System.Net.Mail;
using System.Net;

    public static class EmailSender
    {

        public static void SendNotification(string body, string email)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("techtroj@gmail.com", "FKwK9q4oa9vNFtLqAc4MVMx6P");
            client.Port = 587;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("techtroj@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            mailMessage.Subject = "Regiojet jizdenka";
            client.Send(mailMessage);

        }
    }

