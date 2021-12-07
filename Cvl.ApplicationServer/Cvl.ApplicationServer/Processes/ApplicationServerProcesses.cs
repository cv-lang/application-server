using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Services;
using Cvl.ApplicationServer.Processes.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes
{
    public class ApplicationServerProcesses
    {
        private readonly IFullSerializer _fullSerializer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ProcessInstanceContainerService _processInstanceContainerService;
        private readonly ProcessActivityService _processActivityService;
        private readonly ProcessesApiService _processesApiService;        

        public ApplicationServerProcesses(IFullSerializer fullSerializer, IJsonSerializer jsonSerializer,
            ProcessInstanceContainerService processInstanceContainerService,
            ProcessActivityService processActivityService,
            ProcessesApiService processesApiService)
        {
            this._processInstanceContainerService = processInstanceContainerService;
            this._processActivityService = processActivityService;
            this._processesApiService = processesApiService;
            this._fullSerializer = fullSerializer;
            this._jsonSerializer = jsonSerializer;
        }       

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerAsync(long processId)
        {
            return await _processInstanceContainerService.GetSingleAsync(processId);
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerWithNestedObject(long processId)
        {
            return await _processInstanceContainerService.GetProcessInstanceContainerWithNestedObject(processId);
        }

        public async Task SetStepAsync(long processId, string stepName, string description, int? step)
        {
            await _processInstanceContainerService.UpdateProcessStepAsync(processId, stepName, description, step);
        }

        internal async Task UpdateProcessInstanceContainer(ProcessInstanceContainer container)
        {
            await _processInstanceContainerService.UpdateAsync(container);
        }

        internal async Task<ProcessInstanceContainer> GetProcessInstanceContainer(long processId)
        {
            return await _processInstanceContainerService.GetSingleAsync(processId);
        }

        public IQueryable<ProcessListItemDto> GetAllProcesses()
        {
            return _processesApiService.GetAllProcesses();
        }

        public IQueryable<ProcessActivityDto> GetProcessActivities(long processId)
        {
            return _processesApiService.GetProcessActivities(processId);
        }

        public IQueryable<ProcessStepHistoryDto> GetProcessSteps(long processId)
        {
            return _processesApiService.GetProcessSteps(processId);
        }

        public async Task<TProcesInterface> CreateProcessAsync<TProcesInterface>(ClientConnectionData clientConnectionData)
            where TProcesInterface : class, IProcess
        {
            var process = await _processInstanceContainerService.CreateProcessAsync<TProcesInterface>();
            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData, _fullSerializer, _jsonSerializer, _processInstanceContainerService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }

        internal async Task<TProcesInterface> LoadProcessAsync<TProcesInterface>(long processId, ClientConnectionData clientConnectionData)
             where TProcesInterface : class, IProcess
        {
            var process = await _processInstanceContainerService.LoadProcessAsync<TProcesInterface>(processId);

            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData, _fullSerializer, _jsonSerializer, _processInstanceContainerService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }

        internal async Task<TProcesInterface> LoadProcessAsync<TProcesInterface>(string processNumber, ClientConnectionData clientConnectionData)
             where TProcesInterface : class, IProcess
        {
            var process = await _processInstanceContainerService.LoadProcessAsync<TProcesInterface>(processNumber);

            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData, _fullSerializer, _jsonSerializer, _processInstanceContainerService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }
    }
}
