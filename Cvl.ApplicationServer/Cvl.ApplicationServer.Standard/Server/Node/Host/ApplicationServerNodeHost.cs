using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Jobs.Logic;
using Cvl.ApplicationServer.Server.Node.Processes.Logic;
using Cvl.ApplicationServer.Server.Node.Services.Logic;

namespace Cvl.ApplicationServer.Server.Node.Host
{
    /// <summary>
    /// Host węzła - odpowiada za całość systemu
    /// </summary>
    public class ApplicationServerNodeHost
    {
        public ApplicationServerNodeHost()
        {
            JobsManager = new JobsLogic(this);
            ProcessManager = new ProcessLogic(this);
            ServiceManager = new ServiceLogic(this);
        }
        public JobsLogic JobsManager { get; set; }
        public ProcessLogic ProcessManager { get; set; }
        public ServiceLogic ServiceManager { get; set; }


        /// <summary>
        /// Scieżka do folderu aplikacji np. c:\cvl\application-server\
        /// </summary>
        public string ApplicationServerPath { get; set; }


        #region Metody hosta

        public void Start()
        {
            ServiceManager.Start();
            JobsManager.Start();
            ProcessManager.Start();
            
        }

        #endregion

    }
}
