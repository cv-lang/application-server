using System;
using Cvl.ApplicationServer.Server.Node.Host;

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
            applicationServer.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.TestProcess");
        }
    }
}
