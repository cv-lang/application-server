using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Newtonsoft.Json;

namespace Cvl.ApplicationServer.Processes.Model
{
    [Table("ProcessExternalData", Schema = "Processes")]
    public class ProcessExternalData : BaseEntity
    {
        public ProcessExternalData()
        {
        }

        public long ProcessInstanceId { get; set; }

        [JsonIgnore]
        public virtual ProcessInstanceContainer ProcessInstance { get; set; } = null!;

        /// <summary>
        /// Data from process (to external source)
        /// </summary>
        public string? ProcessOutputDataFullSerialization { get; set; }

        /// <summary>
        /// Data from extrernal source (to process)
        /// </summary>
        public string? ExternalInputDataFullSerialization { get; set; }
    }
}
