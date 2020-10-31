using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.NodeNetwork.Server.Extensions;
using Cvl.NodeNetwork.ServiceHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Server.Extensions
{
    public static class ApplicationServerExtensions
    {
        public static ApplicationServerNodeHost ApplicationServerNodeHost;
        public static IApplicationBuilder UseApplicationServer(this IApplicationBuilder app)
        {
            

            //BaseProcess proces = new JdgProcess();

            NodeNetworkServiceHost.RegisterService<IProcessEngine>(ApplicationServerNodeHost.ProcessManager);
            app.UseNodeNetwork();
            return app;
        }

        public static IServiceCollection UseApplicationServerServices(this IServiceCollection services)
        {
            //application server start
            ApplicationServerNodeHost = new ApplicationServerNodeHost();
            ApplicationServerNodeHost.ApplicationServerPath = @"C:\cvl\application-server";
            ApplicationServerNodeHost.Start();

            var procesId = ApplicationServerNodeHost.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.BankLoanProcess");
            var desc2 = ApplicationServerNodeHost.ProcessManager.GetProcessData(2);
            procesId = ApplicationServerNodeHost.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.SimpleTestProcess");
            desc2 = ApplicationServerNodeHost.ProcessManager.GetProcessData(3);

            services.AddSingleton(typeof(IProcessEngine), ApplicationServerNodeHost.ProcessManager);
            return services;
        }
    }
}
