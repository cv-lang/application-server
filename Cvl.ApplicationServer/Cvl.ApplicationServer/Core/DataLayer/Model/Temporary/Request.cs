using System.ComponentModel.DataAnnotations.Schema;

namespace Cvl.ApplicationServer.Core.DataLayer.Model.Temporary
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
