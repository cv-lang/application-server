using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Emails;
using Cvl.ApplicationServer.Core.Users.Interfaces;
using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Test
{

    public class AddNewUserProcessState
    {
        public Step1Registration? Step1Registration { get; set; }
        public long EmailVerificationCode { get; set; }
    }

    public enum AddNewUserProcessStep
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

    public class SimpleTestProcess : BaseProcess
    {
        private readonly IUsersService _usersService;
        private readonly IEmailSender _emailSender;
        public AddNewUserProcessState State { get; set; }
        
        public SimpleTestProcess(IUsersService usersService, IEmailSender emailSender)
        {
            _usersService = usersService;
            _emailSender = emailSender;
            State = new AddNewUserProcessState();
        }


        public void Start()
        {
            Step1(new Step1Registration() { Email = "test@test.com" });
            VirtualMachine.VirtualMachine.Hibernate();

            Step2("1234");
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


    }
}
