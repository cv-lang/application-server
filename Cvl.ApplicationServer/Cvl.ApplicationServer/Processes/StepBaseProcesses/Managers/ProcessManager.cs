using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Queries;
using ThreadState = Cvl.ApplicationServer.Processes.Core.Threading.ThreadState;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers
{
    internal class ProcessManager : IProcessManager
    {
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;
        public IProcess Process { get; set; } = null!;

        public ProcessManager(ProcessQueries processQueries, IFullSerializer fullSerializer)
        {
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
        }

        private IProcess LoadProcess(string processNumber)
        {
            return _processQueries.LoadProcessAsync<IProcess>(processNumber).Result;
        }

        public object GetExternalData(string processNumber)
        {
            var process = LoadProcess(processNumber);
            var externalDataXml = process.ProcessData.ProcessInstanceContainer
                .ProcessExternalData.ProcessExternalDataFullSerialization;
            if (externalDataXml == null)
            {
                return null;
            }
            else
            {
                var externalData = _fullSerializer.Deserialize<object>(externalDataXml);

                return externalData;
            }
            
        }

        public void SetExternalData(string processNumber, object externalData)
        {
            var process = LoadProcess(processNumber);

            var xml = _fullSerializer.Serialize(externalData);
            process.ProcessData.ProcessInstanceContainer
                .ProcessExternalData.ProcessExternalDataFullSerialization = xml;
        }

        public void SetStep(string stepName, string description, object step)
        {
            Process.ProcessData.ProcessInstanceContainer.Step.Step = Convert.ToInt32(step);
            Process.ProcessData.ProcessInstanceContainer.Step.StepName = stepName;
            Process.ProcessData.ProcessInstanceContainer.Step.StepDescription = description;

            var s = new ProcessStepHistory(Process.ProcessData.ProcessId);
            s.Step.Step = Convert.ToInt32(step);
            s.Step.StepName = stepName;
            s.Step.StepDescription = description;

            Process.ProcessData.ProcessInstanceContainer.ProcessStepHistories.Add(s);
        }

        public void SetToJobThread()
        {
            Process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForExecution;
        }

        public void SetToApiThread()
        {
            Process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.Idle;
        }        
    }
}
