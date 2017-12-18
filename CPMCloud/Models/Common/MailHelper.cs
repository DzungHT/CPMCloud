using System.Net.Mail;
using System.Net;

public class MailHelper
{
    /// <summary>
    /// Sends an mail message
    /// </summary>
    /// <param name="from">Sender address</param>
    /// <param name="to">Recepient address</param>
    /// <param name="bcc">Bcc recepient</param>
    /// <param name="cc">Cc recepient</param>
    /// <param name="subject">Subject of mail message</param>
    /// <param name="body">Body of mail message</param>
    private static MailAddress from = new MailAddress("hvitportaldev1@gmail.com", "Cybertron Team");
    private static string password = "hvit12345678";
    public static void SendEmail(MailAddress to, string subject, string body)
    {
        using (MailMessage mm = new MailMessage(from, to))
        {
            mm.Subject = subject;
            mm.Body = body;

            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            NetworkCredential networkcred = new NetworkCredential(from.Address, password);
            smtp.Credentials = networkcred;
            smtp.Port = 587;
            smtp.Timeout = 20000;
            smtp.Send(mm);
        }
    }
}