using Cvl.ApplicationServer.Core.Model;

namespace Cvl.ApplicationServer.Core.Files.Model
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
