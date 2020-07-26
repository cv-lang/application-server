using Cvl.ApplicationServer.Jobs.Model;
using Cvl.ApplicationServer.ProcessesEngine.Model;
using Cvl.ApplicationServer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Interfaces
{
    public interface IApplicatnioServer
    {
        List<JobDescription> GetJobs();
        List<ProcessTypeDescription> GetProcessTypes();
        List<ServiceDescription> GetServices();
    }
}
