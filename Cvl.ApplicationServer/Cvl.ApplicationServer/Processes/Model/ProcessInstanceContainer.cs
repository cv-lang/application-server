using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Model.Processes
{

    /// <summary>
    /// Process instances container
    /// </summary>
    [Table("ProcessInstanceContainer", Schema = "Processes")]
    [Index(nameof(ProcessNumber))]
    public class ProcessInstanceContainer : BaseEntity
    {
        public ProcessInstanceContainer(string processNumber, string type, string statusName)
        {
            ProcessNumber = processNumber;
            Type = type;
            StatusName = statusName;
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
        public ProcessThreadData ThreadData { get; set; } = new ProcessThreadData();

        /// <summary>
        /// Business specific data
        /// </summary>
        public ProcessBusinessData BusinessData { get; set; } = new ProcessBusinessData();

        /// <summary>
        /// Current step
        /// </summary>
        public ProcessStepData Step { get; set; } = new ProcessStepData(0, "new", "new");

        /// <summary>
        /// Extrenals Id
        /// </summary>
        public ExternalIdentifiers ExternalIds { get; set; } = new ExternalIdentifiers();

        /// <summary>
        /// Data that has different meanings depending on the type of process
        /// </summary>
        public ProcessSpecificData ProcessSpecificData { get; set; } = new ProcessSpecificData();
    }
}
