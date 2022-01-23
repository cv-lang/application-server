using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessInstanceContainerService : BaseService<ProcessInstanceContainer, ProcessInstanceContainerRepository>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProcessActivityDataRepository _processActivityDataRepository;
        private readonly ProcessActivityRepository _processActivityRepository;
        private readonly ProcessStateDataRepository _processInstanceStateDataRepository;
        private readonly ProcessDiagnosticDataRepository _processDiagnosticDataRepository;
        private readonly ProcessStepHistoryRepository _processStepHistoryRepository;
        private readonly IProcessNumberGenerator _processNumberGenerator;
        private readonly ProcessStateDataService _processStateDataService;

        public ProcessInstanceContainerService(ApplicationServerDbContext applicationDbContext, 
            IServiceProvider serviceProvider, 
            ProcessInstanceContainerRepository processInstanceRepository,
            ProcessActivityDataRepository processActivityDataRepository,
            ProcessActivityRepository processActivityRepository,
            ProcessStateDataRepository processInstanceStateDataRepository,
            ProcessDiagnosticDataRepository processDiagnosticDataRepository,
            ProcessStepHistoryRepository processStepHistoryRepository,            
            IProcessNumberGenerator processNumberGenerator,
            ProcessStateDataService processStateDataService
            ) :base(processInstanceRepository)
        {
           _serviceProvider = serviceProvider;
            this._processActivityDataRepository = processActivityDataRepository;
            this._processActivityRepository = processActivityRepository;
            this._processInstanceStateDataRepository = processInstanceStateDataRepository;
            this._processDiagnosticDataRepository = processDiagnosticDataRepository;
            this._processStepHistoryRepository = processStepHistoryRepository;            
            this._processNumberGenerator = processNumberGenerator;
            this._processStateDataService = processStateDataService;
        }

        public async Task UpdateProcessStepAsync(long processId, string stepName, string description, int step)
        {
            ProcessInstanceContainer processInstance = await this.GetSingleAsync(processId);

            if (step != null)
            {
                processInstance.Step.Step = step;
            }
            processInstance.StatusName = stepName;
            processInstance.Step.StepDescription = description;
            Repository.Update(processInstance);

            var procesStepHistory = new ProcessStepHistory(processId) { Step = new ProcessStepData(step, stepName, description) };
            _processStepHistoryRepository.Insert(procesStepHistory);
            await _processStepHistoryRepository.SaveChangesAsync();
        }

        public async Task<T> CreateProcessAsync<T>() where T : class, IProcessOld
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstanceContainer = new ProcessInstanceContainer("", process.GetType().FullName!, "new");

            processInstanceContainer.ProcessInstanceStateData = new ProcessStateData(string.Empty);
            processInstanceContainer.ProcessDiagnosticData = new ProcessDiagnosticData();

            Repository.Insert(processInstanceContainer);
            await Repository.SaveChangesAsync();


            processInstanceContainer.ProcessNumber = _processNumberGenerator.GenerateProcessNumber(processInstanceContainer.Id);
            Repository.Update(processInstanceContainer);
            await Repository.SaveChangesAsync();

            process.ProcessId = processInstanceContainer.Id;
            process.ProcessNumber = processInstanceContainer.ProcessNumber;

            return process;
        }

        public async Task<T> LoadProcessAsync<T>(long processId) where T : class, IProcessOld
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstance = await Repository.GetSingleAsync(processId);
            process.ProcessId = processId;
            process.ProcessNumber = processInstance.ProcessNumber;

            var processState = await _processInstanceStateDataRepository.GetSingleAsync(process.ProcessId);

            _processStateDataService.DeserializeProcess(process, processState.ProcessStateFullSerialization);

            return process;
        }

        public async Task<T> LoadProcessAsync<T>(string processNumber) where T : class, IProcessOld
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstance = await Repository.GetSingleByNumberAsync(processNumber);

            process.ProcessId = processInstance.Id;
            process.ProcessNumber = processInstance.ProcessNumber;

            var processState = await _processInstanceStateDataRepository.GetSingleAsync(process.ProcessId);
            _processStateDataService.DeserializeProcess(process, processState.ProcessStateFullSerialization);

            return process;
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerWithNestedObject(long processId)
        {
            return await Repository.GetAll()
                .Include(x => x.ProcessDiagnosticData)
                .Include(x=> x.ProcessInstanceStateData)
                .SingleAsync(x => x.Id == processId);
        }

        public async Task InsertProcessActivityAsync(ProcessActivity activity)
        {
            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);

            _processActivityRepository.Insert(activity);
            await _processActivityRepository.SaveChangesAsync();
        }

        public async Task UpdateActivityErrorAsync(Exception ex, ProcessActivity activity, ProcessActivityData activityData)
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

        public async Task UpdateActivityResponseAsync(string response, string jsonResponse, string? returnValueType, ProcessActivity activity, ProcessActivityData activityData)
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

        public async Task UpdateProcessActivityAsync(ProcessActivity activity)
        {            
            //zwiększam licznik aktywności procesu
            var processDiagnosticData = _processDiagnosticDataRepository.GetByProcessId(activity.ProcessInstanceId);
            processDiagnosticData.NumberOfActivities++;
            processDiagnosticData.LastRequestPreview = activity.PreviewRequestJson;
            _processDiagnosticDataRepository.Update(processDiagnosticData);
            await _processDiagnosticDataRepository.SaveChangesAsync();
        }

        public async Task UpdateProcessActivityDataAsync(ProcessActivityData activityData)
        {
            _processActivityDataRepository.Update(activityData);
            await _processActivityDataRepository.SaveChangesAsync();
        }
    }
}
