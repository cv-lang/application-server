using System.ComponentModel.DataAnnotations.Schema;
using Cvl.ApplicationServer.Core.Model;
using Newtonsoft.Json;

namespace Cvl.ApplicationServer.Core.Processes.Model
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
        /// External data
        /// </summary>
        public string? ProcessExternalDataFullSerialization { get; set; }
    }
}
