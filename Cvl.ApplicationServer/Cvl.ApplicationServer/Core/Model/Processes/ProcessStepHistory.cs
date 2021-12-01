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
        public ProcessStepHistory(long? processInstanceId, int? step, string stepName, string stepDescription)
        {
            ProcessInstanceId = processInstanceId;
            Step = step;
            StepName = stepName;
            StepDescription = stepDescription;
        }

        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstanceContainer? ProcessInstance { get; set; }
        public int? Step { get; set; }
        public string StepName { get; set; }
        public string StepDescription { get; set; }
    }
}
