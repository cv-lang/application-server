// See https://aka.ms/new-console-template for more information

using ConsoleApp1.TTeeesd;
using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Cvl.ApplicationServer;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Users.Services;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Services;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Workers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polenter.Serialization;
using Polenter.Serialization.Core;
using TestNS;

//konfiguracja
string ApplicationServerContextConnectionString = "";

var hostBuilder = Host.CreateDefaultBuilder(args);
hostBuilder.ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();

        IHostEnvironment env = hostingContext.HostingEnvironment;

        configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        IConfigurationRoot configurationRoot = configuration.Build();

        ApplicationServerContextConnectionString = configurationRoot.GetConnectionString("ApplicationServerContext");

    });


//serwisy
hostBuilder.ConfigureServices(services =>
{
    services.UseRegisterApplicationServer(ApplicationServerContextConnectionString);
    services.AddTransient<StepBaseTestProcess>();
    services.AddTransient<LongRunningTestProcess>();
    services.AddHostedService<LongRunningProcessBackgroundWorker>();
});



//logging
//hostBuilder.ConfigureLogging(builder =>
//    builder
//    .ClearProviders()
//    .AddHierarchicalLogger()
//);




var app = hostBuilder.Build();

var serviceProvider = app.Services;



var worker = serviceProvider.GetService<ILongRunningProcessWorker>();

var processProxyService = serviceProvider.GetRequiredService<IProcessProxyService>();
var longRunningProcesses = serviceProvider.GetRequiredService<ILongRunningProcessesService>();
var p2 = await longRunningProcesses.StartLongRunningProcessAsync<LongRunningTestProcess>(null);

await worker.RunProcessesAsync();
await worker.RunProcessesAsync();

var st1 = await longRunningProcesses.GetProcessStatusAsync(p2.ProcessNumber);

var data1 = await longRunningProcesses.GetProcessExternalDataAsync(p2.ProcessNumber);
await longRunningProcesses.SetProcessExternalDataAsync(p2.ProcessNumber, new ViewResponse() { SelectedAction = "sdffsd" });
await worker.RunProcessesAsync();

using (var p3 = await processProxyService
           .OpenProcessProxyAsync<LongRunningTestProcess>(p2.ProcessNumber))
{
    
    

}




namespace TestNS
{
    public class Test2
    {
        private readonly ILogger<Test> logger;
        public Test2(ILogger<Test> logger)
        {
            logger.LogWarning("z klasy test2");
            this.logger = logger;
        }

        public void TestowaMetoda()
        {
            logger.LogWarning("z klasy test2 TestowaMetoda");
        }
    }

    public class Test
    {

        public Test(string nazwa, int wiek)
        {
            Nazwa = nazwa;
            Wiek = wiek;
        }

        public string Nazwa { get; set; }
        public int Wiek { get; set; }

        public object? Project { get; set; }

        public Dictionary<string, object> Projects { get; set; } = new Dictionary<string, object>();
    }

    public abstract class BaseProject
    {
        public string? Name { get; set; }
    }

    public class JsProject : BaseProject
    {
        public string? Path { get; set; }
    }
}