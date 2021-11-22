// See https://aka.ms/new-console-template for more information
using ConsoleApp1.TTeeesd;
using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Server.Setup;
using Cvl.ApplicationServer.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var t = new Test("test",3);
t.Project = new CProjekt() { Path = "sdfdf"};
t.Projects["dupa"] = new CProjekt();
t.Projects["a"] = new JsProject() { Path = "jspath" };

var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json");
var configuration = builder.Build();

var ProcessesContextConnectionString = configuration.GetConnectionString("ProcessesContext");

var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ProcessesContextConnectionString))
            .UseRegisterApplicationServer()
            .BuildServiceProvider();

var testController= serviceProvider.GetService<TestController>();
var tt = await testController.TestStep1(new TestRequest());
await testController.TestStep2(new TestRequest());

Console.WriteLine(tt);

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