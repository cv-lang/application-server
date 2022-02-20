using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Logging.Logger;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Services;
using Cvl.ApplicationServer.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Emails;
using Cvl.ApplicationServer.Core.Interfaces;
using Cvl.ApplicationServer.Core.Serializers;
using Cvl.ApplicationServer.Core.Users.Commands;
using Cvl.ApplicationServer.Core.Users.Interfaces;
using Cvl.ApplicationServer.Core.Users.Model;
using Cvl.ApplicationServer.Core.Users.Queries;
using Cvl.ApplicationServer.Core.Users.Services;
using Cvl.ApplicationServer.Processes.Commands;
using Cvl.ApplicationServer.Processes.Queries;
using Cvl.ApplicationServer.Processes.Repositories;

namespace Cvl.ApplicationServer.Server.Setup
{    

    public static class ApplicationServerExtensions
    {
        public static IServiceCollection UseRegisterApplicationServer(this IServiceCollection services)
        {
            services.AddTransient<IFullSerializer, XmlFullSerializer>()
                .AddTransient<IJsonSerializer, Cvl.ApplicationServer.Core.Tools.Serializers.JsonSerializer>()
                
                .AddTransient<ProcessActivityDataRepository, ProcessActivityDataRepository>()
                .AddTransient<ProcessActivityRepository, ProcessActivityRepository>()
                .AddTransient<ProcessInstanceContainerRepository, ProcessInstanceContainerRepository>()
                .AddTransient<ProcessStateDataRepository, ProcessStateDataRepository>()
                .AddTransient<ProcessDiagnosticDataRepository, ProcessDiagnosticDataRepository>()
                .AddTransient<ProcessStepHistoryRepository, ProcessStepHistoryRepository>()
                .AddTransient<ProcessExternalDataRepository, ProcessExternalDataRepository>()
                .AddTransient<ProcessInstanceContainerService, ProcessInstanceContainerService>()
                
                .AddTransient<ProcessCommands, ProcessCommands>()
                .AddTransient<ProcessInstanceContainerCommands, ProcessInstanceContainerCommands>()
                .AddTransient<ProcessInstanceContainerQueries, ProcessInstanceContainerQueries>()
                .AddTransient<ProcessStateDataCommands, ProcessStateDataCommands>()
                .AddTransient<ProcessQueries, ProcessQueries>()
                .AddTransient<ProcessExternalDataCommands, ProcessExternalDataCommands>()
                .AddTransient<ProcessStepQueries, ProcessStepQueries>()
                .AddTransient<ProcessActivityQueries>()

                .AddTransient<IApplicationServer, ApplicationServers.ApplicationServer>()
                .AddTransient<Cvl.ApplicationServer.Core.Interfaces.IApplicationServerProcesses,
                    Cvl.ApplicationServer.ApplicationServers.Internals.ApplicationServerProcesses>()

                .AddTransient<Repository<User>, Repository<User>>()
                .AddTransient<UserCommands, UserCommands>()
                .AddTransient<UserQueries, UserQueries>()
                .AddTransient<IUsersService, UsersService>()


                .AddTransient<IEmailSender, EmailSender>()
                //.AddScoped<RequestLoggerScope, RequestLoggerScope>()

                .AddTransient<LogElementRepository, LogElementRepository>()
                .AddTransient<LogPropertiesRepository, LogPropertiesRepository>()
                .AddTransient<LogElementService, LogElementService>()

                .AddTransient<SimpleLongRunningTestProcess, SimpleLongRunningTestProcess>()


                //old - to delete
                .AddTransient<ProcessesApiService, ProcessesApiService>()
                .AddTransient<IProcessNumberGenerator, ProcessNumberGenerator>()
                .AddTransient<IProcessNumberGenerator, ProcessNumberGenerator>()
                //.AddTransient<ApplicationServerProcesses, ApplicationServerProcesses>()
                .AddTransient<ProcessActivityService, ProcessActivityService>()
                .AddTransient<ProcessStepHistoryService, ProcessStepHistoryService>()
                .AddTransient<ProcessStateDataService, ProcessStateDataService>()
                .AddTransient<ProcessStateDataService, ProcessStateDataService>();

            return services;
        }
    }
}
