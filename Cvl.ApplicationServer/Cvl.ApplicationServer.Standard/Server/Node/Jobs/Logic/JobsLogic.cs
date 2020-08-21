using Cvl.ApplicationServer.Base;
using Cvl.ApplicationServer.Server.Node.Host;
using System;

namespace Cvl.ApplicationServer.Server.Node.Jobs.Logic
{
    public class JobsLogic : BaseLogic
    {
        private ApplicationServerNodeHost applicationServerNodeHost;

        public JobsLogic(ApplicationServerNodeHost applicationServerNodeHost)
        {
            this.applicationServerNodeHost = applicationServerNodeHost;
        }

        internal void Start()
        {
        }
    }
}
