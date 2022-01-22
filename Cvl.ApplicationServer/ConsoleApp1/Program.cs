// See https://aka.ms/new-console-template for more information

using ConsoleApp1.TTeeesd;
using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Logging.Logger;
using Cvl.ApplicationServer.Server.Setup;
using Cvl.ApplicationServer.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Text.Json;
using TestNS;

//konfiguracja
string ProcessesContextConnectionString = "";

var hostBuilder = Host.CreateDefaultBuilder(args);
hostBuilder.ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();

        IHostEnvironment env = hostingContext.HostingEnvironment;

        configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        IConfigurationRoot configurationRoot = configuration.Build();

        ProcessesContextConnectionString = configurationRoot.GetConnectionString("ProcessesContext");

    });


//serwisy
hostBuilder.ConfigureServices(services =>
{
    var _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    _optionsBuilder.UseSqlServer(ProcessesContextConnectionString);
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ProcessesContextConnectionString));
    services.UseRegisterApplicationServer();
    //services.AddScoped<Test2, Test2>();
    
});


//logging
hostBuilder.ConfigureLogging(builder =>
    builder
    .ClearProviders()
    .AddHierarchicalLogger()
);




var app = hostBuilder.Build();
using var requestScope = app.Services.CreateScope();
var serviceProvider = requestScope.ServiceProvider;// app.Services;

var logger = serviceProvider.GetService<ILogger<Program>>(); ;
logger.LogWarning("sdfsfd");
using (var scop = logger.BeginScope("scope1"))
{
    var test = serviceProvider.GetService<Test2>();
    logger.LogWarning("sdfsfd w scope1");
    using (var scop2 = logger.BeginScope("scope2"))
    {
        logger.LogWarning("sdfsfd w scope2");
        test.TestowaMetoda();
    }
    logger.LogWarning("sdfsfd2 w scope1");
}

    Console.WriteLine("Hello, World!");



//var t = new Test("test",3);
//t.Project = new CProjekt() { Path = "sdfdf"};
//t.Projects["dupa"] = new CProjekt();

//t.Projects["a"] = new JsProject() { Path = "jspath" };


var s =new Polenter.Serialization.SharpSerializer();



var testController= serviceProvider.GetService<TestController>()!;
var tt = await testController.TestStep1Async(new TestRequest());

//for (int i = 0; i < 100; i++)
{
    var request = new TestRequest() { ProcessNumber = tt.ProcessNumber };
    tt = await testController.TestStep1Async(request);

    await testController.TestStep2Async(request);

    try
    {
        await testController.TestStep3Async(request);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    try
    {
        await testController.TestStep4Async(request);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    try
    {
        await testController.TestStep5Async(request);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


Console.WriteLine(tt);


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