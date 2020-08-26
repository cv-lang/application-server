﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Cvl.ApplicationServer.Base;
using Cvl.ApplicationServer.Server.Node.Host;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.Tools;

namespace Cvl.ApplicationServer.Server.Node.Processes.Logic
{
    /// <summary>
    /// Silnik procesów - wykonujący procesy (instancje procesów)
    /// </summary>
    public class ProcessEngine : BaseLogic , IProcessEngine
    {
        private ApplicationServerNodeHost applicationServerNodeHost;
        private string processesDataPath => applicationServerNodeHost.ApplicationServerDataPath + "\\processes";

        private ProcessesConfiguration configuration;

        private List<ProcessContainer> processesList ;

        private BackgroundWorker timer;

        public ProcessEngine(ApplicationServerNodeHost applicationServerNodeHost)
        {
            this.applicationServerNodeHost = applicationServerNodeHost;
        }

        internal void Start()
        {
            LoadAndSyncConfiguration();
            LoadProcesses();
            SaveProcesses();

            timer = new BackgroundWorker();
            timer.DoWork += Timer_DoWork;
            timer.RunWorkerAsync();

            //.DoWork += EngineBackgroundWorker_DoWork;
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                TestRunProcesses();
                Thread.Sleep(100);
            }
        }

        

        public void TestRunProcesses()
        {
            var processes=processesList
                .Where(x => x.Process.ProcessStatus == EnumProcessStatus.WaitingForExecution);

            foreach (var processContainer in processes)
            {
                processContainer.Process.ModifiedDate = DateTime.Now;
                var result = processContainer.VirtualMachine.Resume<object>();
            }
        }

        #region Configuration
        public void LoadAndSyncConfiguration()
        {
            var configFile = processesDataPath + "\\processes.config";
            if (File.Exists(configFile))
            {
                var xml = File.ReadAllText(configFile);
                configuration = Serializer.DeserializeObject<ProcessesConfiguration>(xml);
            }
            else
            {
                configuration = new ProcessesConfiguration();
            }



            var processes = GetLoadedProcessesType();
            foreach (var process in processes)
            {
                if (configuration.Processes.Any(x => x.ProcessTypeFullName == process.FullName) == false)
                {
                    //niemamy takiego procesu dodajemy go
                    var pt = new ProcessTypeDescription();
                    pt.ProcessTypeFullName = process.FullName;
                    pt.AssemblyQualifiedName = process.AssemblyQualifiedName;
                    configuration.Processes.Add(pt);
                }
            }

            var xml2 = Serializer.SerializeObject(configuration);
            File.WriteAllText(configFile, xml2);
        }

        public IEnumerable<Type> GetLoadedProcessesType()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(BaseProcess)))
                    {
                        yield return type;
                    }
                }
            }
        }

        #endregion

        #region DAL

        private long currentProcessId;
        public void LoadProcesses()
        {
            var filePath = processesDataPath + "\\processesInstances.xml";
            if (File.Exists(filePath))
            {
                var xml = File.ReadAllText(filePath);
                processesList = Serializer.DeserializeObject<List<ProcessContainer>>(xml);
            }
            else
            {
                processesList = new List<ProcessContainer>();
            }

            if (processesList.Any())
            {
                currentProcessId = processesList.Max(x => x.Id) + 1;
            }
            else
            {
                currentProcessId = 1;
            }
        }

        public void SaveProcesses()
        {
            var filePath = processesDataPath + "\\processesInstances.xml";
            var xml = Serializer.SerializeObject(processesList);
            File.WriteAllText(filePath, xml);
        }

        private void addProcess(ProcessContainer container)
        {
            currentProcessId++;

            container.Id = currentProcessId;
            container.Process.Id = currentProcessId;
            processesList.Add(container);
        }

        private ProcessContainer getProcess(long procesId)
        {
            var process= processesList.Single(x => x.Id == procesId);
            return process;
        }

        #endregion


        public long StartProcess(string processName, object inputData = null)
        {
            var typeDescription = configuration.Processes
                .FirstOrDefault(x => x.ProcessTypeFullName== processName);

            var type = Type.GetType(typeDescription.AssemblyQualifiedName);

            var process = Activator.CreateInstance(type) as BaseProcess;
            process.CreatedDate = DateTime.Now;
            process.ModifiedDate = DateTime.Now;
            process.ReadedDate = DateTime.Now;
            
            var container = new ProcessContainer();
            container.Process = process;
            var vm = new VirtualMachine.VirtualMachine();
            vm.LogMonitor = new LogMonitor(process); 
            
            container.VirtualMachine = vm;
            var result = vm.Start<object>("StartProcess",  process, inputData);

            addProcess(container);
            return container.Id;
        }

        public ProcessDescription GetProcessData(long processId)
        {
            var proc=getProcess(processId);

            var desc = new ProcessDescription(proc);

            return desc;
        }

        public void SetProcessData(BaseModel userDataBaseModel)
        {
            var proc = getProcess(userDataBaseModel.ProcessId);
            proc.Process.FormDataFromUser = new FormData("", userDataBaseModel);
            proc.Process.ProcessStatus = EnumProcessStatus.WaitingForExecution;
        }


        public FormData GetProcessFormData(long processId)
        {
            var process = getProcess(processId);
            //process.Process.ProcessStatus == EnumProcessStatus.WaitingForUserData;
            return process.Process.FormDataToShow;
        }

        public void SetProcessFormData(long processId, FormData formData)
        {
            var process = getProcess(processId);
            //process.Process.ProcessStatus == EnumProcessStatus.WaitingForUserData;
            process.Process.FormDataFromUser = formData;
            process.Process.ProcessStatus = EnumProcessStatus.WaitingForExecution;
        }

        public List<ProcessDescription> GetAllProcessesDescriptions()
        {
            var list = processesList.Select(x => new ProcessDescription(x)).ToList();
            return list;
        }

        public BaseProcess GetProcess(long id)
        {
            var process = processesList.FirstOrDefault(x => x.Id == id);
            return process.Process;
        }
    }
}
