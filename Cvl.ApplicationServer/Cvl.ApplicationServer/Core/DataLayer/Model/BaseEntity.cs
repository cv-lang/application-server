using System.ComponentModel.DataAnnotations;

namespace Cvl.ApplicationServer.Core.DataLayer.Model
{
    /// <summary>
    /// Base class for all db entites
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// If true - object is removed
        /// </summary>
        public bool Archival { get; set; } = false;

        /// <summary>
        /// date of object creation
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
