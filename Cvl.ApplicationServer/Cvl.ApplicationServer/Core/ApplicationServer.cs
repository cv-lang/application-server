using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core
{
    public class ApplicationServer
    {
        private readonly IFullSerializer _fullSerializer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ProcessInstanceContainerService _processInstanceContainerService;
        private readonly ProcessService _processService;

        public ApplicationServer(IFullSerializer fullSerializer, IJsonSerializer jsonSerializer,
            ProcessInstanceContainerService processInstanceContainerService,
            ProcessService processService)
        {
            this._processInstanceContainerService = processInstanceContainerService;
            this._processService = processService;
            this._fullSerializer = fullSerializer;
            this._jsonSerializer = jsonSerializer;
        }

        public async Task SetStepAsync(long processId, string stepName, string description, int? step)
        {
            await _processInstanceContainerService.UpdateProcessStepAsync(processId, stepName, description, step);
        }

        public IQueryable<ProcessListItemDto> GetAllProcesses()
        {
            return _processService.GetAllProcesses();
        }

        public async Task<TProcesInterface> CreateProcessAsync<TProcesInterface>(Model.ClientConnectionData clientConnectionData) 
            where TProcesInterface : class, IProcess            
        {
            var process = await _processInstanceContainerService.CreateProcessAsync<TProcesInterface>();
            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData,_fullSerializer, _jsonSerializer, _processInstanceContainerService);

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
    }
}
