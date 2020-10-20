using System;
using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;

namespace Cvl.ApplicationServer.NodeHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //inicialize application-server
            var applicationPath = @"D:\cvl\application-server";
            var applicationServer = new ApplicationServerNodeHost();
            applicationServer.ApplicationServerPath = applicationPath;
            applicationServer.Start(startBackgroundProcessThread:false);


            //Process test
            var procesId = applicationServer.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.BankLoanTestProcess");
            
            //Setp 1
            applicationServer.ProcessManager.TestRunProcesses();
            var data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s1 = data.FormModel.GetModel() as RegistrationStepData;
            s1.Agreements.ForEach(x => x.Accepted = true);
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            //step 2
            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s2 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.EmailValidationData;
            s2.ValidationCodeFromUser = s2.ValidationCode;
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            //step 3
            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s3 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.SmsValidationData;
            s3.ValidationCodeFromUser = s3.ValidationCode;
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            //step 4
            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s4 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.CompanyData;
            s4.CompanyIdentificator = "1112223344";
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            //step 5
            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s5 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.CompanyData;
            s5.CompanyName = "Example company name";
            s5.CompanyCity = "NYC";
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            //test for LLC path
            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s9 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.CompanyData;
            s9.CompanyName = "Example company name";
            s9.CompanyCity = "NYC";
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);
        }
    }
}
