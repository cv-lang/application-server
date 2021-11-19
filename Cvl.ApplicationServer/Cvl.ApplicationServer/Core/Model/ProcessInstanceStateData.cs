using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    public class ProcessInstanceStateData : BaseEntity
    {
        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstance ProcessInstance { get; set; }

        public string XmlState { get; set; }
    }
}
