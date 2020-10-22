using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Processes.Model;

namespace Cvl.ApplicationServer.Server.Node.Processes.Interfaces
{
    public interface IProcessEngine
    {
        long StartProcess(string processName, object inputData = null);
        ProcessDescription GetProcessData(long processId);
        void SetProcessData(FormModel formModel);

        List<ProcessDescription> GetAllProcessesDescriptions();

        List<ProcessTypeDescription> GetAllProcessesTypesDescriptions();

        BaseProcess GetProcess(long id);
    }
}
