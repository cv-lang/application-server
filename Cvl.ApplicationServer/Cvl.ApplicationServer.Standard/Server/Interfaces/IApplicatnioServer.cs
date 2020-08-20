using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Jobs.Model;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.Server.Node.Services.Model;

namespace Cvl.ApplicationServer.Server.Interfaces
{
    public interface IApplicatnioServer
    {
        List<JobDescription> GetJobs();
        List<ProcessTypeDescription> GetProcessTypes();
        List<ServiceDescription> GetServices();
    }
}
