using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cvl.ApplicationServer.Core.Model.Processes
{

    /// <summary>
    /// Process instances container
    /// </summary>
    [Table("ProcessInstanceContainer", Schema = "Processes")]
    public class ProcessInstanceContainer : BaseEntity
    {
        public ProcessInstanceContainer(string processNumber, string type, string statusName, string stepName, string stepDescription)
        {
            ProcessNumber = processNumber;
            Type = type;
            StatusName = statusName;
            StepName = stepName;
            StepDescription = stepDescription;
            ProcessThreadData = new ProcessThreadData();
            BusinessData = new ProcessBusinessData();
            ExternalIds = new ExternalIdentifiers();
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
        /// Object with proces state (serialized)
        /// </summary>
        public virtual ProcessStateData ProcessInstanceStateData { get; set; } = null!;

        /// <summary>
        /// Proces diagnostic data (number of request, last error...)
        /// </summary>
        public virtual ProcessDiagnosticData ProcessDiagnosticData { get; set; } = null!;

        /// <summary>
        /// Main(single) thread state 
        /// </summary>
        public ProcessThreadData ProcessThreadData { get; set; }

        /// <summary>
        /// Business specific data
        /// </summary>
        public ProcessBusinessData BusinessData { get; set; }

        /// <summary>
        /// Extrenals Id
        /// </summary>
        public ExternalIdentifiers ExternalIds { get; set; }
    }
}
