using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessInstanceService : BaseService<ProcessInstance, ProcessInstanceRepository>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProcessActivityDataRepository _processActivityDataRepository;
        private readonly ProcessActivityRepository _processActivityRepository;
        private readonly ProcessInstanceStateDataRepository _processInstanceStateDataRepository;
        private readonly ProcessDiagnosticDataRepository _processDiagnosticDataRepository;
        private readonly IFullSerializer _fullSerializer;

        public ProcessInstanceService(ApplicationDbContext applicationDbContext, 
            IServiceProvider serviceProvider, 
            ProcessInstanceRepository processInstanceRepository,
            ProcessActivityDataRepository processActivityDataRepository,
            ProcessActivityRepository processActivityRepository,
            ProcessInstanceStateDataRepository processInstanceStateDataRepository,
            ProcessDiagnosticDataRepository processDiagnosticDataRepository,
            IFullSerializer fullSerializer

            ) :base(processInstanceRepository)
        {
           _serviceProvider = serviceProvider;
            this._processActivityDataRepository = processActivityDataRepository;
            this._processActivityRepository = processActivityRepository;
            this._processInstanceStateDataRepository = processInstanceStateDataRepository;
            this._processDiagnosticDataRepository = processDiagnosticDataRepository;
            this._fullSerializer = fullSerializer;
        }        

        internal T CreateProcess<T>() where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstance = new ProcessInstance("", process.GetType().FullName!, "new", "init","", 
                Processes.Threading.ThreadState.Idle);

            processInstance.ProcessInstanceStateData = new ProcessStateData(string.Empty);
            processInstance.ProcessDiagnosticData = new ProcessDiagnosticData();

            Repository.Insert(processInstance);
            Repository.SaveChanges();

            process.ProcessId = processInstance.Id;

            return process;
        }

        internal T LoadProcess<T>(long processId) where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            process.ProcessId = processId;
            var processInstance = Repository.Get(processId);

            DeserializeProcess(process, processInstance.ProcessInstanceStateData.ProcessStateFullSerialization);

            return process;
        }        

        internal void InsertProcessActivity(ProcessActivity activity)
        {
            if (activity.ProcessInstanceId is null) { throw new NullReferenceException("ProcessInstanceId"); }

            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId.Value);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);

            _processActivityRepository.Insert(activity);
            _processActivityRepository.SaveChanges();
        }

        internal void UpdateActivityError(Exception ex, ProcessActivity activity, ProcessActivityData activityData)
        {
            
            activityData.ResponseJson = ex.ToString();
            _processActivityDataRepository.Update(activityData);

            activity.PreviewResponseJson = $"Error:{ex.Message} {ex.GetType().FullName}";
            activity.ActivityState = ProcessActivityState.Exception;
            _processActivityRepository.Update(activity);

            //ustawiam last error preview
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId.Value);
            processDiagnosticData.NumberOfErrors++;
            processDiagnosticData.LastError = activityData.ResponseJson;
            processDiagnosticData.LastErrorPreview = activity.PreviewResponseJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);
        }

        internal void UpdateActivityResponse(string response, string jsonResponse, string? returnValueType, ProcessActivity activity, ProcessActivityData activityData)
        {
            if (activity.ProcessInstanceId is null) { throw new NullReferenceException("ProcessInstanceId"); }

            activityData.ResponseFullSerialization = response;
            activityData.ResponseJson = jsonResponse;
            activityData.ResponseType = returnValueType;
            _processActivityDataRepository.Update(activityData);

            activity.ResponseDate = DateTime.Now;
            activity.PreviewResponseJson = jsonResponse.Truncate(ProcessActivity.JsonPreviewSize);
            activity.ActivityState = ProcessActivityState.Executed;
            _processActivityRepository.Update(activity);

            //ustawiam last response preview
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId.Value);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastResponsePreview = activity.PreviewResponseJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);
        }

        internal void UpdateProcessActivity(ProcessActivity activity)
        {
            if (activity.ProcessInstanceId is null) { throw new NullReferenceException("ProcessInstanceId"); }

            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId.Value);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);
        }

        internal void UpdateProcessActivityData(ProcessActivityData activityData)
        {
            _processActivityDataRepository.Update(activityData);
            _processActivityDataRepository.SaveChanges();
        }

        

        internal void SerializeProcess(IProcess process)
        {
            if (process is IProcessSerialization processSerialization)
            {
                var json = processSerialization.ProcessSerizalization(_fullSerializer);
                var stateData = _processInstanceStateDataRepository.GetAll().Single(x => x.Id == process.ProcessId);
                stateData.ModifiedDate = DateTime.Now;
                stateData.ProcessStateFullSerialization = json;
                _processInstanceStateDataRepository.SaveChanges();
            }
        }

        internal void DeserializeProcess(IProcess process, string fullState)
        {
            if (process is IProcessSerialization processSerialization)
            {
                processSerialization.ProcessDeserialization(_fullSerializer, fullState);
            }
        }
    }
}
