namespace Cvl.ApplicationServer.Core.ExternalServices.Emails
{
    public interface IEmailSender
    {
        void SendEmail(string to, string title, string content);
    }
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string to, string title, string content)
        {

        }
    }
}
