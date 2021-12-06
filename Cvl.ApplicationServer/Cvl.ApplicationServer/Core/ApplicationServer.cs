using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
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

        public ApplicationServerProcesses Processes { get; private set; }



        public ApplicationServer(IFullSerializer fullSerializer, IJsonSerializer jsonSerializer,
            ProcessInstanceContainerService processInstanceContainerService,
            ProcessService processService, ApplicationServerProcesses processes)
        {
            this._processInstanceContainerService = processInstanceContainerService;
            this._processService = processService;
            this._fullSerializer = fullSerializer;
            this._jsonSerializer = jsonSerializer;
            Processes = processes;
        }

        
    }
}
