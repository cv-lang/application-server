using Cvl.ApplicationServer.Core.Services;
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

        public ApplicationServer(ProcessService processService)
        {
            this._processService = processService;
        }

        public T CreateProcess<T>()
        {            
            return _processService.CreateProcess<T>();
        }
    }
}
