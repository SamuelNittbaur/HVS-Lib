using System.ComponentModel;
using System.Net.Mail;
using System.Net;

namespace Logic
{
    public static class MailController
    {
        //Die Funktion ist KI-generiert
        public static string GetUserNotificationContent(BorrowableItem item, string user)
        {
            string itemText = string.Empty;
            if (item is DVD) itemText = "die DVD";
            if (item is Book) itemText = "das Buch";

            //Das Design ist KI generiert

            return "<!DOCTYPE html>" +
                            "<html lang=\"de\">" +
                            "<head>" +
                            "    <meta charset=\"UTF-8\">" +
                            "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                            "    <title>Verfügbarkeitshinweis</title>" +
                            "    <style>" +
                            "        body {" +
                            "            font-family: Arial, sans-serif;" +
                            "            background-color: #f4f4f4;" +
                            "            display: flex;" +
                            "            justify-content: center;" +
                            "            align-items: center;" +
                            "            height: 100vh;" +
                            "        }" +
                            "        .container {" +
                            "            background: white;" +
                            "            padding: 20px;" +
                            "            border-radius: 10px;" +
                            "            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);" +
                            "            text-align: center;" +
                            "            max-width: 400px;" +
                            "        }" +
                            "        img {" +
                            "            max-width: 100%;" +
                            "            border-radius: 5px;" +
                            "        }" +
                            "        h2 {" +
                            "            color: #333;" +
                            "        }" +
                            "        p {" +
                            "            color: #555;" +
                            "        }" +
                            "        .btn {" +
                            "            display: inline-block;" +
                            "            padding: 10px 20px;" +
                            "            background-color: #007bff;" +
                            "            color: white;" +
                            "            text-decoration: none;" +
                            "            border-radius: 5px;" +
                            "            margin-top: 10px;" +
                            "        }" +
                            "        .btn:hover {" +
                            "            background-color: #0056b3;" +
                            "        }" +
                            "    </style>" +
                            "</head>" +
                            "<body>" +
                            "    <div class=\"container\">" +
                            "        <img src=\"book-placeholder.jpg\" alt=\"Buch Cover\">" +
                            $"        <h2>{itemText} ist verfügbar!</h2>" +
                            "        <p><strong>\"{item.title}\"</strong> ist jetzt in unserem Buchladen vorrätig.</p>" +
                            "        <p>Kommen Sie vorbei und holen es ab.</p>" +
                            "    </div>" +
                            "</body>" +
                            "</html>";

        }

        public static bool SendEmail(string smtpServer, int smtpPort, string senderEmail, string senderPassword,
                                string recipientEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, body);
                    mail.IsBodyHtml = false;

                    client.Send(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.Message);
                return false;
            }
        }
    }
}
