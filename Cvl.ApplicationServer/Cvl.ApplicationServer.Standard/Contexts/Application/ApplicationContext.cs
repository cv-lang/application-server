using Cvl.ApplicationServer.Contexts.Application.LogAbstraction;
using Cvl.ApplicationServer.Contexts.Application.LogStorageAbstraction;
using Cvl.ApplicationServer.Contexts.FrameworkAbstractions;
using Cvl.ApplicationServer.Contexts.FrameworkAbstractions.Implementations.NetStandard20;
using Cvl.ApplicationServer.Monitoring;
using Cvl.ApplicationServer.Monitoring.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.Application
{
    /// <summary>
    /// Kontekst aplikacji
    /// instancja dla całej aplikacji, serwisu, strony Web, aplikacji desktop...
    /// </summary>
    public class ApplicationContext
    {
        internal IFrameworkAbstraction Framework;
        
        public ApplicationContext(IFrameworkAbstraction framework)
        {
            this.Framework = framework;
            logStorage = new LiteDBLogStorage(this);
        }        

        #region Statyczne i inicjalizacja

        public static ApplicationContext Instance { get; set; }

        /// <summary>
        /// Start aplikacji Cvl
        /// tu ustawiamy wszystkie potrzebne elementy systemów
        /// konfigurację - jeśli brak jest konfiguracji to nieuruchamiamy aplikacji
        /// zgłaszamy wyjątek i zapisujemy w logach windowsowych
        /// </summary>
        /// <param name="framework"></param>
        public static void Start(IFrameworkAbstraction framework = null,
            bool runBackgroundMonitoringThread = true)
        {   
            Instance = new ApplicationContext(framework);
            Instance.Start(runBackgroundMonitoringThread);
        }

        public void Start(bool runBackgroundMonitoringThread = true)
        {
            try
            {
                Configuration = new ApplicationConfiguration(this);
                Configuration.Initialize();

                //ApplicationMonitoring = new ApplicationMonitoring(this);
                //ApplicationMonitoring.Initialize(runBackgroundMonitoringThread);
            }
            catch (Exception ex)
            {
                //Framework.IO.CreateWindowsErrorLog(ex, Configuration.ApplicationName, ApplicationMonitoring?.ApplicationNodeId);
                throw ex;
            }
        }

        #endregion

        #region Moniotring i logi

        public ApplicationMonitor GetApplicationMonitor(string applicationModuleName = null)
        {
            return new ApplicationMonitor(this, applicationModuleName);
        }

        private ILogStorageAbstraction logStorage;
        internal void FlushLogger(Logger logger)
        {
            logStorage.FlushLogger(logger);
        }

        #endregion

        #region Konfiguracja
        public ApplicationConfiguration Configuration { get; set; }


        #endregion

    }
}
