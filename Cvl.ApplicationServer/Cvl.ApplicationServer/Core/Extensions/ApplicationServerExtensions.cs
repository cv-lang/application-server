using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Services;
using Cvl.ApplicationServer.Test;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Server.Setup
{    

    public static class ApplicationServerExtensions
    {
        public static IServiceCollection UseRegisterApplicationServer(this IServiceCollection services)
        {
            services.AddTransient<IFullSerializer, FullSerializer>()
            .AddTransient<IJsonSerializer, Cvl.ApplicationServer.Core.Tools.Serializers.JsonSerializer>()
            .AddTransient<ProcessActivityDataRepository, ProcessActivityDataRepository>()
            .AddTransient<ProcessActivityRepository, ProcessActivityRepository>()
            .AddTransient<ProcessInstanceRepository, ProcessInstanceRepository>()
            .AddTransient<ProcessInstanceStateDataRepository, ProcessInstanceStateDataRepository>()
            .AddTransient<ProcessDiagnosticDataRepository, ProcessDiagnosticDataRepository>()
            .AddTransient<ProcessInstanceService, ProcessInstanceService>()
            .AddTransient<ProcessService, ProcessService>()

            .AddTransient<TestService, TestService>()
            .AddTransient<ITestProcess, TestProcess>()
            .AddTransient<TestController, TestController>()
            .AddTransient<Core.ApplicationServer, Core.ApplicationServer>();

            return services;
        }
    }
}
