using Cvl.ApplicationServer.Base;
using Cvl.ApplicationServer.Server.Node.Host;
using System;

namespace Cvl.ApplicationServer.Server.Node.Services.Logic
{
    public class ServiceLogic : BaseLogic
    {
        private ApplicationServerNodeHost applicationServerNodeHost;

        public ServiceLogic(ApplicationServerNodeHost applicationServerNodeHost)
        {
            this.applicationServerNodeHost = applicationServerNodeHost;
        }

        internal void Start()
        {
            
        }
    }
}
