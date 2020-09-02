using System;
using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;

namespace Cvl.ApplicationServer.NodeHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationPath = @"D:\cvl\application-server";
            var applicationServer = new ApplicationServerNodeHost();
            applicationServer.ApplicationServerPath = applicationPath;
            applicationServer.Start();
            var procesId = applicationServer.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.TestProcess");
            
            applicationServer.ProcessManager.TestRunProcesses();
            var data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s1 = data.FormModel.GetModel() as FirstStepData;
            s1.Agreements.ForEach(x => x.Accepted = true);
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            applicationServer.ProcessManager.TestRunProcesses();
            data = applicationServer.ProcessManager.GetProcessFormData(procesId);
            var s2 = data.FormModel.GetModel() as Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps.SmsValidationData;
            s2.ValidationCodeFromUser = s2.ValidationCode;
            applicationServer.ProcessManager.SetProcessFormData(procesId, data);

            applicationServer.ProcessManager.TestRunProcesses();
        }
    }
}
