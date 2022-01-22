using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;

namespace Cvl.ApplicationServer.Files.Model
{
    public class BaseElement : BaseEntity
    {
        public BaseElement()
        {}
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ExternalGroupName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
