using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.NodeNetwork.Server.Extensions;
using Cvl.NodeNetwork.ServiceHost;
using Microsoft.AspNetCore.Builder;
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
            //application server start
            ApplicationServerNodeHost = new ApplicationServerNodeHost();
            ApplicationServerNodeHost.ApplicationServerPath = @"C:\cvl\application-server";
            ApplicationServerNodeHost.Start();

            //BaseProcess proces = new JdgProcess();

            NodeNetworkServiceHost.RegisterService<IProcessEngine>(ApplicationServerNodeHost.ProcessManager);
            app.UseNodeNetwork();
            return app;
        }
    }
}
