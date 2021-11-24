using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
{
    [Table("ProcessStateData", Schema = "Processes")]
    public class ProcessStateData : BaseEntity
    {
        public ProcessStateData(string processStateFullSerialization)
        {
            ProcessStateFullSerialization = processStateFullSerialization;
        }

        public long ProcessInstanceId { get; set; }
        public virtual ProcessInstance ProcessInstance { get; set; }

        /// <summary>
        /// Full serializet of request
        /// to deserialize an object of type 'object'
        /// </summary>
        public string ProcessStateFullSerialization { get; set; }
    }
}
