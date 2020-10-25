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
    public class ApplicationServerNodeHost : IApplicationServerNodeHost
    {
        public ApplicationServerNodeHost()
        {
            JobsManager = new JobsLogic(this);
            ProcessManager = new ProcessEngine(this);
            ServiceManager = new ServiceLogic(this);
        }
        public JobsLogic JobsManager { get; set; }
        public ProcessEngine ProcessManager { get; set; }
        public ServiceLogic ServiceManager { get; set; }


        /// <summary>
        /// Scieżka do folderu aplikacji np. c:\cvl\application-server\
        /// </summary>
        public string ApplicationServerPath { get; set; }

        public bool UseConfiguration { get; set; } = true;

        public string ApplicationServerDataPath => ApplicationServerPath + "\\data";

        #region Metody hosta

        public void Start(bool startBackgroundProcessThread = true)
        {
            ServiceManager.Start();
            JobsManager.Start();
            ProcessManager.Start(startBackgroundProcessThread);
        }

        #endregion

    }
}
