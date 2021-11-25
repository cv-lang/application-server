﻿using Cvl.ApplicationServer.Core.Database.Contexts;
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
        private readonly ProcessStepHistoryRepository _processStepHistoryRepository;
        private readonly IFullSerializer _fullSerializer;

        public ProcessInstanceService(ApplicationDbContext applicationDbContext, 
            IServiceProvider serviceProvider, 
            ProcessInstanceRepository processInstanceRepository,
            ProcessActivityDataRepository processActivityDataRepository,
            ProcessActivityRepository processActivityRepository,
            ProcessInstanceStateDataRepository processInstanceStateDataRepository,
            ProcessDiagnosticDataRepository processDiagnosticDataRepository,
            ProcessStepHistoryRepository processStepHistoryRepository,
            IFullSerializer fullSerializer

            ) :base(processInstanceRepository)
        {
           _serviceProvider = serviceProvider;
            this._processActivityDataRepository = processActivityDataRepository;
            this._processActivityRepository = processActivityRepository;
            this._processInstanceStateDataRepository = processInstanceStateDataRepository;
            this._processDiagnosticDataRepository = processDiagnosticDataRepository;
            this._processStepHistoryRepository = processStepHistoryRepository;
            this._fullSerializer = fullSerializer;
        }

        internal async Task UpdateProcessStepAsync(long processId, string stepName, string description, int? step)
        {
            ProcessInstance processInstance = await this.GetSingleAsync(processId);

            if (step != null)
            {
                processInstance.Step = step.Value;
            }
            processInstance.StatusName = stepName;
            processInstance.StepDescription = description;
            Repository.Update(processInstance);

            var procesStepHistory = new ProcessStepHistory(processId, step, stepName, description);
            _processStepHistoryRepository.Insert(procesStepHistory);
            await _processStepHistoryRepository.SaveChangesAsync();
        }

        internal async Task<T> CreateProcessAsync<T>() where T : class, IProcess
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
            await Repository.SaveChangesAsync();

            process.ProcessId = processInstance.Id;

            return process;
        }

        internal async Task<T> LoadProcessAsync<T>(long processId) where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            process.ProcessId = processId;
            var processInstance = await Repository.GetSingleAsync(processId);

            DeserializeProcess(process, processInstance.ProcessInstanceStateData.ProcessStateFullSerialization);

            return process;
        }        

        internal async Task InsertProcessActivityAsync(ProcessActivity activity)
        {
            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);

            _processActivityRepository.Insert(activity);
            await _processActivityRepository.SaveChangesAsync();
        }

        internal async Task UpdateActivityErrorAsync(Exception ex, ProcessActivity activity, ProcessActivityData activityData)
        {
            
            activityData.ResponseJson = ex.ToString();
            _processActivityDataRepository.Update(activityData);

            activity.PreviewResponseJson = $"Error:{ex.Message} {ex.GetType().FullName}";
            activity.ActivityState = ProcessActivityState.Exception;
            _processActivityRepository.Update(activity);

            //ustawiam last error preview
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfErrors++;
            processDiagnosticData.LastError = activityData.ResponseJson;
            processDiagnosticData.LastErrorPreview = activity.PreviewResponseJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);

            //zapisuje całość UoW
            await Repository.SaveChangesAsync();
        }

        internal async Task UpdateActivityResponseAsync(string response, string jsonResponse, string? returnValueType, ProcessActivity activity, ProcessActivityData activityData)
        {
            activityData.ResponseFullSerialization = response;
            activityData.ResponseJson = jsonResponse;
            activityData.ResponseType = returnValueType;
            _processActivityDataRepository.Update(activityData);

            activity.ResponseDate = DateTime.Now;
            activity.PreviewResponseJson = jsonResponse.Truncate(ProcessActivity.JsonPreviewSize);
            activity.ActivityState = ProcessActivityState.Executed;
            _processActivityRepository.Update(activity);

            //ustawiam last response preview
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastResponsePreview = activity.PreviewResponseJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);

            //zapisuje całość UoW
            await Repository.SaveChangesAsync();
        }

        internal async Task UpdateProcessActivityAsync(ProcessActivity activity)
        {            
            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);
            await _processDiagnosticDataRepository.SaveChangesAsync();
        }

        internal async Task UpdateProcessActivityDataAsync(ProcessActivityData activityData)
        {
            _processActivityDataRepository.Update(activityData);
            await _processActivityDataRepository.SaveChangesAsync();
        }

        

        internal async Task SerializeProcessAsync(IProcess process)
        {
            if (process is IProcessSerialization processSerialization)
            {
                var json = processSerialization.ProcessSerizalization(_fullSerializer);
                var stateData = _processInstanceStateDataRepository.GetAll().Single(x => x.Id == process.ProcessId);
                stateData.ModifiedDate = DateTime.Now;
                stateData.ProcessStateFullSerialization = json;
                await _processInstanceStateDataRepository.SaveChangesAsync();
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