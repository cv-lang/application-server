using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    public class StepHistory : BaseEntity
    {        
        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstance ProcessInstance { get; set; }
        public string Step { get; set; }
        public string StepDescription { get; set; }
    }
}
