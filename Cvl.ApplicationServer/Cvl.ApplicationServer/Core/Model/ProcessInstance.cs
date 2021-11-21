using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cvl.ApplicationServer.Core.Model
{

    /// <summary>
    /// Process instances
    /// </summary>
    [Table("ProcessInstance", Schema = "Processes")]
    public class ProcessInstance : BaseEntity
    {
        public ProcessInstance() { }

        public ProcessInstance(string processNumber, string type, string status, string step, string stepDescription,
            Processes.Threading.ThreadState mainThreadState)
        {
            ProcessNumber = processNumber;
            Type = type;
            Status = status;
            Step = step;
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
        public string Status { get; set; }

        /// <summary>
        /// User-friendly process step name
        /// </summary>
        public string Step { get; set; }

        /// <summary>
        /// User-friendly process step description
        /// </summary>
        public string StepDescription { get; set; }

        /// <summary>
        /// Main(single) thread state 
        /// </summary>
        public Processes.Threading.ThreadState MainThreadState { get; set; }

        public virtual ProcessInstanceStateData ProcessInstanceStateData { get; set; }
    }
}
