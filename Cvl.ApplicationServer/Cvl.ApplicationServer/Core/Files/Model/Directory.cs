using System.ComponentModel.DataAnnotations.Schema;

namespace Cvl.ApplicationServer.Core.Files.Model
{
    public class Directory : BaseElement
    {
        
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Directory? Parent { get; set; }
    }
}
