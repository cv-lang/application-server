using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
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

        public ProcessService(ApplicationDbContext applicationDbContext, 
            IServiceProvider serviceProvider, 
            ProcessInstanceRepository processInstanceRepository,
            ProcessActivityDataRepository processActivityDataRepository,
            ProcessActivityRepository processActivityRepository

            ) :base(applicationDbContext)
        {
           _serviceProvider = serviceProvider;
            this._processInstanceRepository = processInstanceRepository;
            this._processActivityDataRepository = processActivityDataRepository;
            this._processActivityRepository = processActivityRepository;
        }


        internal T CreateProcess<T>() where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;

            var processInstance = new ProcessInstance("", typeof(T).FullName!, "new", "init","", 
                Processes.Threading.ThreadState.Idle);

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
    }
}
