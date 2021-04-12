using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Base.Model
{
    public class BaseObject
    {
        public long Id { get; set; }
        public bool Archival { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
