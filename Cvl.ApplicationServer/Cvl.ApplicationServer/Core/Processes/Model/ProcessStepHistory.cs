using System.ComponentModel.DataAnnotations.Schema;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;

namespace Cvl.ApplicationServer.Core.Processes.Model
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
