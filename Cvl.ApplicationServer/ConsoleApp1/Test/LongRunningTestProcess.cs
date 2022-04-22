using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ExternalServices.Emails;
using Cvl.ApplicationServer.Core.Processes;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Users.Services;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Test
{
    public class LongRunningTestProcess : ILongRunningProcess
    {
        #region Constructor and private serices
        private readonly ILongRunningProcessManager _processManager;
        private readonly IUsersService _usersService;
        private readonly IEmailSender _emailSender;
        public LongRunningTestProcess(ILongRunningProcessManagerFactory processManagerFactory, IUsersService usersService, IEmailSender emailSender)
        {
            _processManager = processManagerFactory.CreateProcessManager(this);
            _usersService = usersService;
            _emailSender = emailSender;
            State = new AddNewUserProcessState();
        }
        #endregion

        #region Process state
        public ProcessData? ProcessData { get; set; }
        public AddNewUserProcessState State { get; set; }
        #endregion
        

        [Interpret]
        public LongRunningProcessResult StartProcess(object inputParam)
        {
            _processManager.SetStep("start", "start", LongRunningTestProcessStep.Init);

            Step1(new Step1Registration() { Email = "test@test.com", Password = "sdf"});
            _processManager.Delay(DateTime.UtcNow.AddSeconds(1));

            _processManager.SetStep("Step 2", "Step 2 descrption", LongRunningTestProcessStep.Registration);

            Step2("1234");
            var dataFromOutside = _processManager.ShowView(new View("test"));
            CheckResult(dataFromOutside);

            _processManager.WaitForExternalData($"Test data from extrenalSource " +dataFromOutside);


            _processManager.SetStep("Step 3", "Step 3 descrption", LongRunningTestProcessStep.EmailVerification);
            //Delay(DateTime.Now.AddSeconds(1));

            var response = _processManager.ShowView(new View("registration"));
            CheckResult(response);
            return new LongRunningProcessResult() { Result = default};
        }

        private void CheckResult(object o)
        {
        }

        public void Step1(Step1Registration request)
        {
            State.Step1Registration = request;
            State.EmailVerificationCode = DateTime.Now.Ticks % 10000;
            _emailSender.SendEmail(request.Email, "Weryfikacja emaila",
                $"Twój kod weryfikacji: {State.EmailVerificationCode}");
        }

        public void Step2(string emailVerificationCode)
        {
           _usersService.RegisterNewUserAsync(State.Step1Registration.Email, State.Step1Registration.Password)
               .Wait();
        }

        protected LongRunningTestProcessStep Step
        {
            get
            {
                return (LongRunningTestProcessStep)(ProcessData?.Step ?? throw new Exception("ProcessData not loaded"));
            }
            set
            {
                if (ProcessData == null)
                    throw new Exception("ProcessData not loaded");
                ProcessData.Step = (int)value;
            }
        }
    }


    public class AddNewUserProcessState
    {
        public Step1Registration? Step1Registration { get; set; }
        public long EmailVerificationCode { get; set; }
    }

    public enum LongRunningTestProcessStep
    {
        Init = 0,
        Registration = 1,
        EmailVerification = 2,
    }
    public class Step1Registration
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
