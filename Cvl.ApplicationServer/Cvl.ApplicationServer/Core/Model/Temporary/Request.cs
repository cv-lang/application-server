using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Temporary
{
    [Table("Request", Schema = "Temporary")]
    public class Request : BaseEntity
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string RequestJson { get; set; }


        public DateTime? ResponseDate { get; set; }

        public string? ResponseJson { get; set; }
    }
}
