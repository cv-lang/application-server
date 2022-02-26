using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Temporary
{
    [Table("LogProperties", Schema = "Temporary")]
    public class LogProperties : BaseEntity
    {
        public long CurrentLogId { get; set; }
    }
}
