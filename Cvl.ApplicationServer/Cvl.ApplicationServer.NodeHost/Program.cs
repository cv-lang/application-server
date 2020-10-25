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
            applicationServer.UseConfiguration = false;
            applicationServer.ApplicationServerPath = applicationPath;
            applicationServer.Start(startBackgroundProcessThread:false);


            //Process test
            var procesId = applicationServer.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.BankLoanProcess");

            var test = new BankLoanProcessUnitTest();
            test.Test(applicationServer, procesId);


        }
    }
}
