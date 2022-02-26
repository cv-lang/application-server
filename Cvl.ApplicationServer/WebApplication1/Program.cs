using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Server.Setup;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

var applicationServerContext = builder.Configuration.GetConnectionString("ApplicationServerContext");

// Add services to the container.
builder.Services.UseApplicationServer(applicationServerContext);
//builder.Logging.ClearProviders();
//builder.Logging.AddHierarchicalLogger();

builder.Services.AddKendo();
builder.Services.AddMvc();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

app.Run();
