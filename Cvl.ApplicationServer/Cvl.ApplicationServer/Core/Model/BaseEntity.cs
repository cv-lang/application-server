using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    /// <summary>
    /// Base class for all db entites
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// If true - object is removed
        /// </summary>
        public bool Archival { get; set; } = false;

        /// <summary>
        /// date of object creation
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
