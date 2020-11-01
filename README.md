# .NET application-server
<img src="https://raw.githubusercontent.com/cv-lang/application-server/master/wiki/dotnet-application-server-process.png" alt=".net application server"/> 

### Run your long-running business process

.NET Application-server is a program that manages your business applications,  native workflow, jobs, processes and performs the “magic” of converting .Net Console Application into native workflow that can be run on it.

Like IIS, the application-server is responsible for running your business processes,native workflows, jobs and services. Provides uniform methods for communication, logging, debugging and managing the entire system. It allows you to run business processes on different server nodes (on different physical machines).

# .Net Native workflow
.Net Native workflow is similar to .Net Console Application but has two differences:
- Can communicate with the frontend to display more complex business forms and enter multiple data simultaneously. Frontends supported: Asp.Net Core MVC, Blazor server-site, Blazor Client-site, WPF.
- It has the ability to suspend execution while waiting for user data or external events.. This pause can last for seconds, days, months, years without using CPU or RAM resources - it only uses harddrive to store execution state.

Under the hood, it used a .Net Virtual Machine to hibernate the entire process while it waited and save it to storage like a hard drive. After an external event (user interaction, timers ...), the process is loaded from storage into the virtual machine and restored. 
After the process is hibernated it can be restored on another machine.

# Example of a simple native workflow

Below is an example process with three steps. 1) View and download user data. 2) Put the process to sleep for 2 years. 3) Send an email to the address provided earlier.

View from the process - step 1:

<img src="https://raw.githubusercontent.com/cv-lang/application-server/master/wiki/simpleTestPRocessView.png" alt="simple test process view" width="360px"/> 

Code for the sample native workflow process:

```csharp
public class SimpleTestProcess : BaseProcess
{
    protected override object Start(object inputParameter)
    {            
        //prepare model to show in MVC frontend
        var model = new HelloWorlsModel();  //code below
        model.MessageToTheWorld = "Hello World";
        model.AnotherMessage = "Message from process";
        model.DateTimeFromProcess = DateTime.Now;

        //show MVC view HelloWorldView with model
        var response = ShowForm("HelloWorldView", model); //we show form and waiting for user to submit form

        //after user submit form, save to property user data 
        UserEmail = response.UserEmail;
        UserMessage = response.MessageFromUser;

        //go to deep sleep - 2 years
        Sleep(TimeSpan.FromDays(365 * 2));

        //after wakeup if it's  Sunday
        if ( DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            //go to sleep for a day
            Sleep(TimeSpan.FromDays(1));
        }

        //after 2 year sleeping, send email to user with notification
        sendEmail();

        //end process
        return null;
    }

    private void sendEmail()
    {
        Log($"Sending email to user {UserEmail}, {UserMessage}");

        //you should do real sending ...
        //var emailService = new EmailService();
        //emialService.SendEmail(UserEmail,UserMessage);
    }

    #region Process properties
    public string UserEmail { get; set; }
    public string UserMessage { get; set; }
    #endregion
}

public class HelloWorlsModel
{
    public string MessageToTheWorld { get; set; }
    public string AnotherMessage { get; set; }
    public DateTime DateTimeFromProcess { get; set; }
    public string UserEmail { get; set; }
    public string MessageFromUser { get; set; }
}
```

# Advantages and disadvantages of Native workflow
**Advantages of Native workflow:**
- are structural 1: 1 with BPMN diagrams
- they have all the necessary functionalities in one place (sub-processes, jobs, timers, etc.)
- they are very easy to write - you write like a console application
- they are very easy to debug - you can run them like a console application
- they are very easy to test (as opposed to e.g. Windows Workflow Foundation)

**Disadvantages:**
- If you've read the 'Example of a simple native workflow' section, you may be wondering how to put a process to sleep for 2 years without having to run the entire server for 2 years?
The answer is - virtual machine. Thanks to it, it is possible to hibernate the process and restore it later. This virtualization has some performance impact. It is just as fast as other alternatives such as Windows Workflow Foundation etc.


# Example of the customer's loan process
Let's assume that we have a customer who wants to get a bank loan. The diagram below provides a general overview of all steps in the process. The steps are performed by the customer and/or the bank's system.
This diagram is drawn in the Business Process Model Notation (BPMN - https://en.wikipedia.org/wiki/Business_Process_Model_and_Notation) and starts in the top left blank circle.
Actions and steps are represented by a rounded rectangle and performed in the direction of the arrow. So first we have "Registration and approvals ...", then "Send verification code by e-mail ..." and so on. 
These kinds of diagrams do not show all the details. They are used to show high-level functionality. The BPMN diagram is then "converted" by the developer to the native workflow / business process shown in Diagram 2.

<img width="65%" src="https://raw.githubusercontent.com/cv-lang/application-server/master/wiki/loanProcessDiagram.png" alt=".net application server"/>
Diagram 1 - BPMN Bank loan process

<img width="65%" src="https://raw.githubusercontent.com/cv-lang/application-server/master/wiki/loanProcessNativeWorkflow.png" alt=".net application server"/>
Diagram 2 - Native workflow in C#


Code for Registration step:
```csharp
protected void Registration()
{
    SetStepData("Registration", "Get email, phone number and agreements from Custromer");
    var registration = getRegistrationStepModel();
    var registrationResponse = ShowForm("Registration", registration);

    SetStepData("Registration-response", "Custromer put contact data");
    Log("check, just in case, whether approved consents");
    validateRegistrationStepResponse(registrationResponse);

    SetStepData("Registration-approve", "Custromer approve consents");

    Log("Save the Customer selections");
    SelectedProduct = registrationResponse.SelectedProduct;
    ClientEmail = registrationResponse.Email;
    ClientPhoneNumber = registrationResponse.PhoneNumber;
}
```


# Solution description
- Cvl.ApplicationServer.NodeHost - NodeNetwork module
- Cvl.ApplicationServer.NodeHostWeb - Hosting application of NodeNetwork module 
- **Cvl.ApplicationServer.Server** - main part for server  ( contains all staf needet for create simple application serwer node)
- **Cvl.ApplicationServer.Standard** - core part of application server 
- Cvl.ApplicationServer.TestBuisnessProcesses - Automation test for buisness process
- Cvl.ApplicationServer.UnityTest- test for application server logics
- Cvl.ApplicationServer.WpfConsole - WPF application to managment server 
- Cvl.ApplicationServer.WpfConsoleCore - WPF Core application to managment

## NuGet 'Cvl.ApplicationServer'
PM> Install-Package Cvl.ApplicationServer -Version 0.9.1

[NuGet package Cvl.VirtualMachine](https://www.nuget.org/packages/Cvl.ApplicationServer/)

PM> Install-Package Cvl.ApplicationServer.Server -Version 0.9.1

[NuGet package Cvl.VirtualMachine.Server](https://www.nuget.org/packages/Cvl.ApplicationServer.Server/)

## Dependencies

[.NET virtual-machine](https://github.com/cv-lang/virtual-machine)

[.NET node-network](https://github.com/cv-lang/node-network)
