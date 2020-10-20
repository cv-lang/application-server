using System;
using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps;

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

            //test for LLC path - in test mode, all function needet attention from the host-
            //hibernate the process
            applicationServer.ProcessManager.TestRunProcesses(); //create 1-st subproces
            applicationServer.ProcessManager.TestRunProcesses(); //create 2-nd subprocess
            applicationServer.ProcessManager.TestRunProcesses();

            #region Child process execution id:3 ,4

            //process 3 - 1-st person
            var childProcessData1 = applicationServer.ProcessManager.GetProcessFormData(3);
            var c1s1 = childProcessData1.FormModel.GetModel() as PersonData;
            c1s1.Name = "Person 1 Name";
            c1s1.PersonIdentificator = "3";
            applicationServer.ProcessManager.SetProcessFormData(3, childProcessData1);
            applicationServer.ProcessManager.TestRunProcesses();

            childProcessData1 = applicationServer.ProcessManager.GetProcessFormData(3);
            var c1s2 = childProcessData1.FormModel.GetModel() as ApplicationAcceptData;
            c1s2.AcceptAsPerson = true;
            applicationServer.ProcessManager.SetProcessFormData(3, childProcessData1);
            applicationServer.ProcessManager.TestRunProcesses();



            //process 3 - 2-nd person
            var childProcessData2 = applicationServer.ProcessManager.GetProcessFormData(4);
            var c2s1 = childProcessData2.FormModel.GetModel() as PersonData;
            c2s1.Name = "Person 1 Name";
            c2s1.PersonIdentificator = "3";
            applicationServer.ProcessManager.SetProcessFormData(4, childProcessData2);
            applicationServer.ProcessManager.TestRunProcesses();

            childProcessData2 = applicationServer.ProcessManager.GetProcessFormData(4);
            var c2s2 = childProcessData2.FormModel.GetModel() as ApplicationAcceptData;
            c2s2.AcceptAsPerson = true;
            applicationServer.ProcessManager.SetProcessFormData(4, childProcessData2);
            applicationServer.ProcessManager.TestRunProcesses();

            #endregion

            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s9 = data.FormModel.GetModel() as ApplicationAcceptData;
            s9.AcceptAsCompany = true;
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);
            applicationServer.ProcessManager.TestRunProcesses();
        }
    }
}
