using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    [Table("ProcessStepHistory", Schema = "Processes")]
    public class ProcessStepHistory : BaseEntity
    {
        public ProcessStepHistory(long? processInstanceId)
        {
            ProcessInstanceId = processInstanceId;
            Step = new ProcessStepData(null, "new", "new");
        }

        //public ProcessStepHistory(long? processInstanceId, ProcessStepData step)
        //{
        //    ProcessInstanceId = processInstanceId;
        //    Step = step;
        //}

        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstanceContainer? ProcessInstance { get; set; }
        public ProcessStepData Step { get; set; } = null!;
    }
}
