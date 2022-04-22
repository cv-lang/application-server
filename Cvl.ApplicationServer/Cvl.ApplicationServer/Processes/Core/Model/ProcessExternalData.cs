using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Cvl.ApplicationServer.Core.DataLayer.Model;

namespace Cvl.ApplicationServer.Processes.Core.Model
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
