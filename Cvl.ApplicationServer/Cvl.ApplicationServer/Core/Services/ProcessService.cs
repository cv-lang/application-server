﻿using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessService : BaseService<ProcessInstance>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProcessInstanceRepository _processInstanceRepository;
        private readonly ProcessActivityDataRepository _processActivityDataRepository;
        private readonly ProcessActivityRepository _processActivityRepository;
        private readonly ProcessInstanceStateDataRepository _processInstanceStateDataRepository;
        private readonly IFullSerializer _fullSerializer;

        public ProcessService(ApplicationDbContext applicationDbContext, 
            IServiceProvider serviceProvider, 
            ProcessInstanceRepository processInstanceRepository,
            ProcessActivityDataRepository processActivityDataRepository,
            ProcessActivityRepository processActivityRepository,
            ProcessInstanceStateDataRepository processInstanceStateDataRepository,
            IFullSerializer fullSerializer

            ) :base(applicationDbContext)
        {
           _serviceProvider = serviceProvider;
            this._processInstanceRepository = processInstanceRepository;
            this._processActivityDataRepository = processActivityDataRepository;
            this._processActivityRepository = processActivityRepository;
            this._processInstanceStateDataRepository = processInstanceStateDataRepository;
            this._fullSerializer = fullSerializer;
        }


        internal T CreateProcess<T>() where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;

            var processInstance = new ProcessInstance("", typeof(T).FullName!, "new", "init","", 
                Processes.Threading.ThreadState.Idle);

            processInstance.ProcessInstanceStateData = new ProcessInstanceStateData("");

            _processInstanceRepository.Insert(processInstance);
            _processInstanceRepository.SaveChanges();

            process.ProcessId = processInstance.Id;

            return process;
        }

        internal void Insert(ProcessActivityData activityData)
        {
            _processActivityDataRepository.Insert(activityData);
            _processActivityDataRepository.SaveChanges();
        }

        internal void Insert(ProcessActivity activity)
        {
            _processActivityRepository.Insert(activity);
            _processActivityRepository.SaveChanges();
        }

        internal void Update(ProcessActivityData activityData)
        {
            _processActivityDataRepository.Update(activityData);
            _processActivityDataRepository.SaveChanges();
        }

        internal void SerializeProcess(IProcess process)
        {
            if (process is IProcessSerialization processSerialization)
            {
                var json = processSerialization.ProcessSerizalization(_fullSerializer);
                var stateData = _processInstanceStateDataRepository.GetAll().Single(x => x.Id == process.ProcessId);
                stateData.ModifiedDate = DateTime.Now;
                stateData.ProcessStateFullSerialization = json;
                _processInstanceStateDataRepository.SaveChanges();
            }
        }
    }
}