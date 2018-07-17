
using System.Net.Mail;
using System.Net;

    public static class EmailSender
    {

        public static void SendNotification()
        {
        SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("rockinghorse66@gmail.com", "NepredstavitelnaSkoda120");
        client.Port = 587;
        client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("rockinghorse66@gmail.com");
            mailMessage.To.Add("trojaon@gmail.com");
            mailMessage.Body = "Jizdenka";
            mailMessage.Subject = "Regiojet jizdenka";
            client.Send(mailMessage);

        }
    }

