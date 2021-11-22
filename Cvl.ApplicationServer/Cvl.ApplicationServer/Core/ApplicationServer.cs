using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core
{
    public class ApplicationServer
    {
        private readonly ProcessService _processService;
        private readonly IFullSerializer _fullSerializer;
        private readonly IJsonSerializer _jsonSerializer;

        public ApplicationServer(ProcessService processService, IFullSerializer fullSerializer, IJsonSerializer jsonSerializer)
        {
            this._processService = processService;
            this._fullSerializer = fullSerializer;
            this._jsonSerializer = jsonSerializer;
        }

        public IQueryable<ProcessInstance> GetAllProcesses()
        {
            return _processService.GetAllObjects();
        }

        public TProcesInterface CreateProcess<TProcesInterface>(Model.ClientConnectionData clientConnectionData) 
            where TProcesInterface : class, IProcess            
        {
            var process = _processService.CreateProcess<TProcesInterface>();
            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData,_fullSerializer, _jsonSerializer, _processService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }

        internal TProcesInterface LoadProcess<TProcesInterface>(long processId, ClientConnectionData clientConnectionData)
             where TProcesInterface : class, IProcess
        {
            var process = _processService.LoadProcess<TProcesInterface>(processId);

            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData, _fullSerializer, _jsonSerializer, _processService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }
    }
}
