using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Emails
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
