﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
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
        public DateTime CreateTime { get; set; } 

        private ApplicationServerNodeHost applicationServerNodeHost;
        private string processesDataPath => applicationServerNodeHost.ApplicationServerDataPath + "\\processes";

        
        private ProcessesConfiguration configuration;

        private List<ProcessContainer> processesList ;

        //private BackgroundWorker timer;
        static System.Timers.Timer aTimer;

        public ProcessEngine(ApplicationServerNodeHost applicationServerNodeHost)
        {
            this.applicationServerNodeHost = applicationServerNodeHost;
            CreateTime = DateTime.Now;
        }

        internal void Start(bool startBackgroundProcessThread)
        {
            LoadAndSyncConfiguration();
            LoadProcesses();
            SaveProcesses();

            if (startBackgroundProcessThread)
            {
                //timer = new BackgroundWorker();
                //timer.DoWork += Timer_DoWork;
                //timer.RunWorkerAsync();

                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer(8000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }

            //.DoWork += EngineBackgroundWorker_DoWork;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            TestRunProcesses();
        }

        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                TestRunProcesses();
                Thread.Sleep(200);
            }
        }

        

        public void TestRunProcesses()
        {
            var processes=processesList
                .Where(x => x.Process.ProcessStatus == EnumProcessStatus.WaitingForExecution &&
                x.Process.ExecutionDate < DateTime.Now).ToList();

            foreach (var processContainer in processes)
            {
                processContainer.Process.ModifiedDate = DateTime.Now;
                var result = processContainer.VirtualMachine.Resume<object>();
            }

            //przechodze procesy wymajające uwagi Hosta
            processes = processesList
                .Where(x => x.Process.ProcessStatus == EnumProcessStatus.WaitingForHost).ToList();

            foreach (var processContainer in processes)
            {
                processContainer.Process.ModifiedDate = DateTime.Now;
                var parameters = processContainer.VirtualMachine.GetHibernateParams();
                var typ = (EnumHostCommand)parameters[0];

                object resumeParameter = null;
                switch(typ)
                {
                    case EnumHostCommand.CreateChildProcess:
                        var childProcess = (BaseProcess)parameters[1];
                        addProcess(childProcess);
                        resumeParameter = childProcess.Id;
                        processContainer.VirtualMachine.Resume<object>(resumeParameter);
                        break;
                    case EnumHostCommand.WaitForAllChildrenEnd:
                        var areExecuted = processesList.Where(x => x.Process.ParentId == processContainer.Id)
                            .All(x => x.Process.ProcessStatus == EnumProcessStatus.Executed);
                        if(areExecuted)
                        {
                            processContainer.VirtualMachine.Resume<object>(resumeParameter);
                        } 
                        break;
                }                
            }
        }

        

        #region Configuration
        public void LoadAndSyncConfiguration()
        {
            var configFile = processesDataPath + "\\processes.config";

            if (applicationServerNodeHost.UseConfiguration)
            {
                if (File.Exists(configFile))
                {
                    var xml = File.ReadAllText(configFile);
                    configuration = Serializer.DeserializeObject<ProcessesConfiguration>(xml);
                }
                else
                {
                    configuration = new ProcessesConfiguration();
                }
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

            if (applicationServerNodeHost.UseConfiguration)
            {
                var xml2 = Serializer.SerializeObject(configuration);
                File.WriteAllText(configFile, xml2);
            }
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
            if (applicationServerNodeHost.UseConfiguration)
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
            if (applicationServerNodeHost.UseConfiguration)
            {
                var filePath = processesDataPath + "\\processesInstances.xml";
                var xml = Serializer.SerializeObject(processesList);
                File.WriteAllText(filePath, xml);
            }
        }

        private void addProcess(ProcessContainer container)
        {
            currentProcessId++;

            container.Id = currentProcessId;
            container.Process.Id = currentProcessId;
            processesList.Add(container);
        }

        private void addProcess(BaseProcess childProcess)
        {
            var container = createProcessContainer(null, childProcess);
            addProcess(container);
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
                .FirstOrDefault(x => x.ProcessTypeFullName == processName);

            var type = Type.GetType(typeDescription.AssemblyQualifiedName);

            var process = Activator.CreateInstance(type) as BaseProcess;
            ProcessContainer container = createProcessContainer(inputData, process);

            addProcess(container);
            //container.VirtualMachine.Resume<object>();
            return container.Id;
        }

        private static ProcessContainer createProcessContainer(object inputData, BaseProcess process)
        {
            process.CreatedDate = DateTime.Now;
            process.ModifiedDate = DateTime.Now;
            process.ReadedDate = DateTime.Now;

            var container = new ProcessContainer();
            container.Process = process;
            var vm = new VirtualMachine.VirtualMachine();
            vm.LogMonitor = new LogMonitor(process);
            
            container.VirtualMachine = vm;
            var result = vm.Start<object>("StartProcess", process, inputData);
            return container;
        }

        public ProcessDescription GetProcessData(long processId)
        {
            var proc=getProcess(processId);

            var desc = new ProcessDescription(proc);

            return desc;
        }

        public void SetProcessData(FormViewModel userFormModel)
        {
            var proc = getProcess(userFormModel.ProcessId);
            proc.Process.ProcessUI.FormDataFromUser = new FormData("", userFormModel);
            proc.Process.ProcessStatus = EnumProcessStatus.WaitingForExecution;
        }


        public FormData GetProcessFormData(long processId)
        {
            var process = getProcess(processId);
            //process.Process.ProcessStatus == EnumProcessStatus.WaitingForUserData;
            return process.Process.ProcessUI.FormDataToShow;
        }

        public void SetProcessFormData(long processId, FormData formData)
        {
            var process = getProcess(processId);
            //process.Process.ProcessStatus == EnumProcessStatus.WaitingForUserData;
            process.Process.ProcessUI.FormDataFromUser = formData;
            process.Process.ProcessStatus = EnumProcessStatus.WaitingForExecution;
        }

        //
        public List<ProcessDescription> GetAllProcessesDescriptions()
        {
            var list = processesList.Select(x=> new ProcessDescription(x)).ToList();
            return list;
        }

        public List<ProcessTypeDescription> GetAllProcessesTypesDescriptions()
        {
            var list = configuration.Processes;
            return list;
        }

        public BaseProcess GetProcess(long id)
        {
            var process = processesList.FirstOrDefault(x => x.Id == id);
            return process.Process;
        }
    }
}
