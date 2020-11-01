# Getting Started
To create buissness process - Native workflow and run it on application-server we have several options:
- We can create application-server with WPF frontend
- We can create application-server with ASP.NET Blazor (server and client-wasm) frontend
- We can create application-server with WPF frontend
- We can create application-server with ASP.NET Core MVC Web Application frontend.

## ASP.NET Core MVC Web Application frontend
### Create app
1) Create ASP.NET Core Web Application (Model-View-Controller) (ASP.NET Core 3.1) with name 'ExampleApplicationServer'
2) Install NuGet package '**Cvl.ApplicationServer.Server**' and '**Cvl.VirtualMachine**'
3) In Startup.cs add -> using Cvl.ApplicationServer.Server.Extensions;
4) In Startup.cs->ConfigureServices add -> services.UseApplicationServerServices();
5) In Startup.cs->Configure add -> app.UseApplicationServer();
At this moment, ju can run your server (F5) and enter /process-types (https://localhost:44390/process-types) to get list of avaliable process types
There by 2 example processes Cvl.ApplicationServer.Server.Node.Processes.TestProcess.BankLoanProcess and Cvl.ApplicationServer.Server.Node.Processes.TestProcess.SimpleTestProcess.

### Create process
6) Create your simple process, by creating folder Processes and add class MyTestProcess base on Cvl.ApplicationServer.Server.Node.Processes.Model.BaseProcess
7) Creat simple MyModel class with property string MyMessage
8) Override object Start(object inputParameter) and show form -ShowForm("MyTestForm", new MyModel() { MyMessage = "My message from process" });

```CSharp
public class MyModel
{        
    public string MessageFromProcess { get; set; }
    public string UserMessage { get; set; }
}
public class MyTestProcess : Cvl.ApplicationServer.Server.Node.Processes.Model.BaseProcess
{
    protected override object Start(object inputParameter)
    {
        var response = ShowForm("MyTestForm", new MyModel() { MessageFromProcess = "My message from process" });

        return null;
    }
}
```

9) Run application (F5) and enter /process-list (https://localhost:44390/process-types) - you get your process (ExampleApplicationServer.Processes.MyTestProcess)
### Create view for your process
10) In SolutionExpolore in folder Views add floder 'Process'
11) Add Waiting view - in folder Process add razor view 'WaitingView.cshtml'
```html
<h1>The process is running</h1>
<p>Please refresh the page in a moment</p>
```
12) Add MyTestForm view - in folder Process add razor view 'MyTestForm.cshtml'
```html
@model Cvl.ApplicationServer.Server.Node.Processes.Model.FormModel<ExampleApplicationServer.Processes.MyModel>

<div class="row">
    <div class="col-lg-8 mx-auto">
        <h1>My test view</h1>
        <p>Message from process: @Model.Model.MessageFromProcess</p>
        <form action="/process-setdata" method="post">
            @Html.HiddenFor(x => x.ProcessId)
            <div class="form-group mt-5">
                <h3>User message</h3>
            </div>
            <div class="form-group">
                <input asp-for="Model.UserMessage" placeholder="Enter some message" class="form-control" />
            </div>
            <button class="btn btn-primary btn-block mt-2" type="submit"> Confirm </button>
        </form>
    </div>
</div>
```
13) Run your app - F5 - and in browers open link /process-start/ExampleApplicationServer.Processes.MyTestProcess (https://localhost:44390/process-start/ExampleApplicationServer.Processes.MyTestProcess).
This start your process and redirect You to your waiting page (https://localhost:44390/process-view/4) refresh page, you see your form (MyTestForm) with data from process.
14) Enter user message and confirm
15) in this moment in your process - response variable have your input data
16) Let's modyfy process, and add another ShowForm
```CSharp
public class MyTestProcess : Cvl.ApplicationServer.Server.Node.Processes.Model.BaseProcess
{
    protected override object Start(object inputParameter)
    {
        var response = ShowForm("MyTestForm", new MyModel() { MessageFromProcess = "My message from process" });

        ShowForm("MyTestForm", new MyModel() { 
            MessageFromProcess = $"User message '{response.UserMessage}'" });

        return null;
    }
}
```


