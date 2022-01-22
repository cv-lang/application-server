using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Files.Model;

namespace Cvl.ApplicationServer.Documents.Model
{
    public class File : BaseElement
    {
        public string VirtualPath { get; set; }
        public string PhisicalPath { get; set; }
    }
}
