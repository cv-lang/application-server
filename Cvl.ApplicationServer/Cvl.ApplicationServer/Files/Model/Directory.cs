using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;

namespace Cvl.ApplicationServer.Files.Model
{
    public class Directory : BaseElement
    {
        
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Directory? Parent { get; set; }
    }
}
