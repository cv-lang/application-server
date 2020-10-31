using Cvl.ApplicationServer.Server.Node.Processes.Model;
using System;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess
{
    public class HelloWorldModel
    {
        public string MessageToTheWorld { get; set; }
        public string AnotherMessage { get; set; }
        public DateTime DateTimeFromProcess { get; set; }
        public string UserEmail { get; set; }
        public string MessageFromUser { get; set; }
    }

    public class SimpleTestProcess : BaseProcess
    {
        #region Process properties
        public string UserEmail { get; set; }
        public string UserMessage { get; set; }
        #endregion

        protected override object Start(object inputParameter)
        {            
            //prepare model to show in MVC frontend
            var model = new HelloWorldModel();
            model.MessageToTheWorld = "Hello World";
            model.AnotherMessage = "Message from process";
            model.DateTimeFromProcess = DateTime.Now;

            //show MVC view HelloWorldView with model
            var response = ShowForm("HelloWorldView", model); //we show form and waiting for user to submit form
            
            //after user submit form, save to property user data 
            UserEmail = response.UserEmail;
            UserMessage = response.MessageFromUser;

            //go to deep sleep - 2 years
            Sleep(TimeSpan.FromDays(365 * 2));

            //after wakeup if it's  Sunday
            if ( DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                //go to sleep for a day
                Sleep(TimeSpan.FromDays(1));
            }

            //after 2 year sleeping, send email to user with notification
            sendEmail();

            //end process
            return null;
        }

        private void sendEmail()
        {
            Log($"Sending email to user {UserEmail}, {UserMessage}");

            //you should do real sending ...
            //var emailService = new EmailService();
            //emialService.SendEmail(UserEmail,UserMessage);
        }
    }
}
