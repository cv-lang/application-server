using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Model.OwnedClasses;

namespace Cvl.ApplicationServer.Core.Model
{
    [Table("ProcessStepHistory", Schema = "Processes")]
    public class ProcessStepHistory : BaseEntity
    {
        public ProcessStepHistory(long? ProcessInstanceContainerId)
        {
            ProcessInstanceContainerId = ProcessInstanceContainerId;
            Step = new ProcessStepData(0, "new", "new");
        }

        //public ProcessStepHistory(long? processInstanceId, ProcessStepData step)
        //{
        //    ProcessInstanceId = processInstanceId;
        //    Step = step;
        //}

        public long? ProcessInstanceContainerId { get; set; }
        public virtual ProcessInstanceContainer? ProcessInstanceContainer { get; set; }
        public ProcessStepData Step { get; set; } = null!;
    }
}
