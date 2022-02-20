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
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Interfaces;
using Cvl.ApplicationServer.Core.Users.Interfaces;
using Cvl.ApplicationServer.Processes.UI;
using Newtonsoft.Json;
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
    var _optionsBuilder = new DbContextOptionsBuilder<ApplicationServerDbContext>();
    _optionsBuilder.UseSqlServer(ApplicationServerContextConnectionString);
    services.AddDbContext<ApplicationServerDbContext>(options => options.UseSqlServer(ApplicationServerContextConnectionString));
    services.UseRegisterApplicationServer();
    //services.AddScoped<Test2, Test2>();

});


//logging
//hostBuilder.ConfigureLogging(builder =>
//    builder
//    .ClearProviders()
//    .AddHierarchicalLogger()
//);




var app = hostBuilder.Build();
using var requestScope = app.Services.CreateScope();
var serviceProvider = requestScope.ServiceProvider;// app.Services;

var userCommand = serviceProvider.GetService<IUsersService>();
await userCommand.AddRootUserAsync();



var appServer = serviceProvider.GetService<IApplicationServer>();

var t1 = appServer.Processes.StartLongRunningProcess<SimpleLongRunningTestProcess>(4);

var numberOfExecutedProcesses = 0;
numberOfExecutedProcesses = appServer.Processes.RunProcesses();

await Task.Delay(1001);
numberOfExecutedProcesses = appServer.Processes.RunProcesses();

var e1 = appServer.Processes.GetExternalData(t1.ProcessNumber);
numberOfExecutedProcesses = appServer.Processes.RunProcesses();
appServer.Processes.SetExternalData(t1.ProcessNumber, "Testowe dane wejścowe dla procesu");
numberOfExecutedProcesses = appServer.Processes.RunProcesses();

var e2 = appServer.Processes.GetExternalData(t1.ProcessNumber);
appServer.Processes.SetExternalData(t1.ProcessNumber);
numberOfExecutedProcesses = appServer.Processes.RunProcesses();
numberOfExecutedProcesses = appServer.Processes.RunProcesses();

var viewData= appServer.Processes.GetViewData(t1.ProcessNumber);
appServer.Processes.SetViewResponse(t1.ProcessNumber,new ViewResponse(){SelectedAction = "test"});
numberOfExecutedProcesses = appServer.Processes.RunProcesses();

Console.WriteLine("sdf");

//var testProcess = appServer.Processes.CreateProcess<SimpleTestProcess>();



//var testProcess = serviceProvider.GetService<SimpleTestProcess>();

//testProcess.Step1(new Step1Registration(){Email = "sdf", Password = "sdf"});

//appServer.Processes.SaveProcess(testProcess);

//testProcess = (SimpleTestProcess) appServer.Processes.LoadProcess(testProcess.ProcessData.ProcessNumber);

//testProcess.Step2("sdfsdf");




//var serializer = new SharpSerializer();

//serializer.InstanceCreator = new ServiceProviderInstanceCreator(serviceProvider);

//serializer.PropertyProvider.AttributesToIgnore.Clear();
//// remove default ExcludeFromSerializationAttribute for performance gain
//serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));
//var xml = "";
//byte[] bajty = null;
//using (var ms = new MemoryStream())
//{
//    serializer.Serialize(testProcess, ms);
//    ms.Position = 0;
//    bajty = ms.ToArray();
//    xml = Encoding.UTF8.GetString(bajty, 0, bajty.Length);
//}


//bajty = Encoding.UTF8.GetBytes(xml);
//using (var ms = new MemoryStream(bajty))
//{
//    object obiekt = serializer.Deserialize(ms);

//    testProcess = obiekt as SimpleTestProcess;
//}


//var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
//var json = JsonConvert.SerializeObject(testProcess, settings);


//settings = new JsonSerializerSettings
//{
//    TypeNameHandling = TypeNameHandling.All,
//    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
//};
//testProcess = JsonConvert.DeserializeObject<object>(json, settings) as SimpleTestProcess;


//testProcess.Step2("sdfsdf");






//var logger = serviceProvider.GetService<ILogger<Program>>(); ;
//logger.LogWarning("sdfsfd");
//using (var scop = logger.BeginScope("scope1"))
//{
//    var test = serviceProvider.GetService<Test2>();
//    logger.LogWarning("sdfsfd w scope1");
//    using (var scop2 = logger.BeginScope("scope2"))
//    {
//        logger.LogWarning("sdfsfd w scope2");
//        test.TestowaMetoda();
//    }
//    logger.LogWarning("sdfsfd2 w scope1");
//}

//    Console.WriteLine("Hello, World!");



////var t = new Test("test",3);
////t.Project = new CProjekt() { Path = "sdfdf"};
////t.Projects["dupa"] = new CProjekt();

////t.Projects["a"] = new JsProject() { Path = "jspath" };





//var testController= serviceProvider.GetService<TestController>()!;
//var tt = await testController.TestStep1Async(new TestRequest());

////for (int i = 0; i < 100; i++)
//{
//    var request = new TestRequest() { ProcessNumber = tt.ProcessNumber };
//    tt = await testController.TestStep1Async(request);

//    await testController.TestStep2Async(request);

//    try
//    {
//        await testController.TestStep3Async(request);

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }

//    try
//    {
//        await testController.TestStep4Async(request);

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }

//    try
//    {
//        await testController.TestStep5Async(request);

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//}


//Console.WriteLine(tt);


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