using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;

namespace Example.BlazorWasm.Processes
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var ApplicationServer = new ApplicationServerNodeHost();
            ApplicationServer.UseConfiguration = false;
            ApplicationServer.Start(startBackgroundProcessThread: true);
            var procesId = ApplicationServer.ProcessManager.StartProcess("Cvl.ApplicationServer.Server.Node.Processes.TestProcess.BankLoanProcess");
            var desc2 = ApplicationServer.ProcessManager.GetProcessData(2);


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(typeof(IProcessEngine), ApplicationServer.ProcessManager);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
