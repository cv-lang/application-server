using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Cvl.ApplicationServer.Core.DataLayer.Model;

namespace Cvl.ApplicationServer.Processes.Core.Model
{
    [Table("ProcessStateData", Schema = "Processes")]
    public class ProcessStateData : BaseEntity
    {
        public ProcessStateData(string processStateFullSerialization)
        {
            ProcessStateFullSerialization = processStateFullSerialization;
        }

        public long ProcessInstanceId { get; set; }

        [JsonIgnore]
        public virtual ProcessInstanceContainer ProcessInstance { get; set; } = null!;

        /// <summary>
        /// Full serializet of request
        /// to deserialize an object of type 'object'
        /// </summary>
        public string ProcessStateFullSerialization { get; set; }

        public string? ProcessResultFullSerialization { get; set; }
    }
}
