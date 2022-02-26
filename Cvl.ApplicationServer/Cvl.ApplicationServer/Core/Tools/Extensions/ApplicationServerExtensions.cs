using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.ExternalServices.Emails;
using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.Repositories;
using Cvl.ApplicationServer.Core.Processes.Services;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Serializers;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Core.Users.Commands;
using Cvl.ApplicationServer.Core.Users.Model;
using Cvl.ApplicationServer.Core.Users.Queries;
using Cvl.ApplicationServer.Core.Users.Services;
using Cvl.ApplicationServer.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.ApplicationServer.Core.Extensions
{

    public static class ApplicationServerExtensions
    {
        public static IServiceCollection UseRegisterApplicationServer(this IServiceCollection services)
        {

            //proceses
            services
                .AddTransient<ProcessActivityDataRepository, ProcessActivityDataRepository>()
                .AddTransient<ProcessActivityRepository, ProcessActivityRepository>()
                .AddTransient<ProcessInstanceContainerRepository, ProcessInstanceContainerRepository>()
                .AddTransient<ProcessStateDataRepository, ProcessStateDataRepository>()
                .AddTransient<ProcessDiagnosticDataRepository, ProcessDiagnosticDataRepository>()
                .AddTransient<ProcessStepHistoryRepository, ProcessStepHistoryRepository>()
                .AddTransient<ProcessExternalDataRepository, ProcessExternalDataRepository>()

                .AddTransient<IProcessNumberGenerator, ProcessNumberGenerator>()
                .AddTransient<ProcessCommands, ProcessCommands>()
                .AddTransient<ProcessInstanceContainerCommands, ProcessInstanceContainerCommands>()
                .AddTransient<ProcessInstanceContainerQueries, ProcessInstanceContainerQueries>()
                .AddTransient<ProcessStateDataCommands, ProcessStateDataCommands>()
                .AddTransient<ProcessQueries, ProcessQueries>()
                .AddTransient<ProcessExternalDataCommands, ProcessExternalDataCommands>()
                .AddTransient<ProcessStepQueries, ProcessStepQueries>()
                .AddTransient<ProcessActivityQueries>()
                
                .AddTransient<IProcessManager, ProcessManager>()
                .AddTransient<ILongRunningProcessManager, LongRunningProcessManager>();

            //users
            services
                .AddTransient<Repository<User>, Repository<User>>()
                .AddTransient<UserCommands, UserCommands>()
                .AddTransient<UserQueries, UserQueries>()
                .AddTransient<IUsersService, UsersService>();


            //extrenal sevices
            services
                .AddTransient<IEmailSender, EmailSender>()
                //.AddScoped<RequestLoggerScope, RequestLoggerScope>()

                .AddTransient<LogElementRepository, LogElementRepository>()
                .AddTransient<LogPropertiesRepository, LogPropertiesRepository>();

            services
                .AddTransient<IApplicationServer, ApplicationServers.ApplicationServer>()
                .AddTransient<IApplicationServerProcesses, ApplicationServerProcesses>();


            //tools
            services.AddTransient<IFullSerializer, XmlFullSerializer>()
                .AddTransient<IJsonSerializer, JsonSerializer>();

            return services;
        }
    }
}
