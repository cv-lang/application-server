using System.ComponentModel.DataAnnotations.Schema;

namespace Cvl.ApplicationServer.Core.DataLayer.Model.Temporary
{
    [Table("LogProperties", Schema = "Temporary")]
    public class LogProperties : BaseEntity
    {
        public long CurrentLogId { get; set; }
    }
}
