using System.Net;
using System.Net.Mail;
public class Email
{
    public Email()
    {
    }
    public static void SendEmail(string EmailReciever, string EmailSubject, string EmailBody)
    {
        string SenderMail = "info@somana.ir";
        string Password = "SY@UE#QI$Z";

        SmtpClient SMTP = new SmtpClient("185.192.112.12", 25);
        SMTP.Credentials = new NetworkCredential(SenderMail, Password);

        MailMessage Mail = new MailMessage();
        Mail.Body = EmailBody;
        Mail.From = new MailAddress(SenderMail);
        Mail.Subject = EmailSubject;
        Mail.To.Add(EmailReciever);
        Mail.IsBodyHtml = true;
        SMTP.Send(Mail);
    }
}