using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    /// <summary>
    /// History of interaction with process
    /// Contains request and respons of interaction
    /// in json and xml
    /// </summary>
    public class ProcessActivity : BaseEntity
    {
        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstance ProcessInstance { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientIpPort { get; set; }
        public string ClientConnectionData { get; set; }


        public DateTime ActionDate { get; set; } = DateTime.Now;
        public DateTime? RequestDate { get; set; }

        [StringLength(150)]
        public string PreviewRequestJson { get; set; }
        public DateTime? ResponseDate { get; set; }

        [StringLength(150)]
        public string PreviewResponseJson { get; set; }

        public long ProcessActivityDataId { get; set; }
        public ProcessActivityData ProcessActivityData { get; set; }
    }
}
