using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Core.ExternalServices.Emails;
using Cvl.ApplicationServer.Core.Tools.Serializers;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Core.Users.Commands;
using Cvl.ApplicationServer.Core.Users.Model;
using Cvl.ApplicationServer.Core.Users.Queries;
using Cvl.ApplicationServer.Core.Users.Services;
using Cvl.ApplicationServer.Processes.Core.Commands;
using Cvl.ApplicationServer.Processes.Core.Queries;
using Cvl.ApplicationServer.Processes.Core.Repositories;
using Cvl.ApplicationServer.Processes.Core.Services;
using Cvl.ApplicationServer.Processes.Core.Services.ProcessesController;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Factories;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Managers;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Proxies;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Services;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Workers;
using Cvl.ApplicationServer.Processes.StepBaseProcesses;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Factories;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.ApplicationServer.Setup
{

    public static class ApplicationServerExtensions
    {
        public static IServiceCollection UseRegisterApplicationServer(this IServiceCollection services,
            string applicationServerContextConnectionString)
        {
            //db context
            services.AddDbContext<ApplicationServerDbContext>(options =>
                options.UseNpgsql(applicationServerContextConnectionString));


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
                .AddTransient<ILongRunningProcessManager, LongRunningProcessManager>()
                .AddTransient<LongRunningProcessManager, LongRunningProcessManager>() //wymgana dla VM
                .AddTransient<ILongRunningProcessWorker, LongRunningProcessWorker>();

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
                .AddTransient<IApplicationServerSimpleProcesses, ApplicationServerSimpleProcesses>()
                .AddTransient<ILongRunningProcessesService, LongRunningProcessesService>()
                .AddTransient<LongRunningProcessesService>()
                .AddTransient<IProcessesControllerService, ProcessesControllerService>()
                .AddTransient<IProcessManagerFactory, ProcessManagerFactory>()
                .AddTransient<ILongRunningProcessManagerFactory, LongRunningProcessManagerFactory>()
                .AddTransient<IProcessProxyService, ProcessProxyService>();


            //tools
            services.AddTransient<IFullSerializer, XmlFullSerializer>()
                .AddTransient<IJsonSerializer, JsonSerializer>();

            return services;
        }
    }
}
