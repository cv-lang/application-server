using System.ComponentModel.DataAnnotations.Schema;
using Cvl.ApplicationServer.Core.DataLayer.Model;
using Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Model
{

    /// <summary>
    /// Process instances container
    /// </summary>
    [Table("ProcessInstanceContainer", Schema = "Processes")]
    [Index(nameof(ProcessNumber))]
    public class ProcessInstanceContainer : BaseEntity
    {
        public ProcessInstanceContainer(string processNumber, string statusName)
        {
            ProcessNumber = processNumber;
            StatusName = statusName;
        }

        /// <summary>
        /// User-friendly process number
        /// </summary>
        public string ProcessNumber { get; set; }

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
        /// Process external data
        /// </summary>
        public virtual ProcessExternalData ProcessExternalData { get; set; } = null!;

        public virtual ICollection<ProcessStepHistory> ProcessStepHistories { get; set; } = new HashSet<ProcessStepHistory>();

        public ProcessTypeData ProcessTypeData { get; set; } = new ProcessTypeData();

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
