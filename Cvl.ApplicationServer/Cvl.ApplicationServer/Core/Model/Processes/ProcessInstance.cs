using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cvl.ApplicationServer.Core.Model.Processes
{

    /// <summary>
    /// Process instances
    /// </summary>
    [Table("ProcessInstance", Schema = "Processes")]
    public class ProcessInstance : BaseEntity
    {
        public ProcessInstance(string processNumber, string type, string statusName, string stepName, string stepDescription,
            Cvl.ApplicationServer.Processes.Threading.ThreadState mainThreadState)
        {
            ProcessNumber = processNumber;
            Type = type;
            StatusName = statusName;
            StepName = stepName;
            StepDescription = stepDescription;
            MainThreadState = mainThreadState;
        }        


        /// <summary>
        /// User-friendly process number
        /// </summary>
        public string ProcessNumber { get; set; }

        /// <summary>
        /// Process full type name
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// User-friendly process status
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// process step
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// User-friendly process step name
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// User-friendly process step description
        /// </summary>
        public string StepDescription { get; set; }

        /// <summary>
        /// Main(single) thread state 
        /// </summary>
        public Cvl.ApplicationServer.Processes.Threading.ThreadState MainThreadState { get; set; }

        public virtual ProcessStateData ProcessInstanceStateData { get; set; } = null!;

        public virtual ProcessDiagnosticData ProcessDiagnosticData { get; set; } = null!;

        public ProcessBusinessData BusinessData { get; set; } = null!;

        public ExternalIdentifiers ExternalIds { get; set; } = null!;
    }
}
