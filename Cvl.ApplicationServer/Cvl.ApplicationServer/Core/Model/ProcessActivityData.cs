using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    /// <summary>
    /// Activity data - request and respons data
    /// all json's, xml's
    /// </summary>
    public class ProcessActivityData : BaseEntity
    {
        public string RequestXml { get; set; }
        public string RequestJson { get; set; }
        public string RequestType { get; set; }


        public string ResponseXml { get; set; }
        public string ResponseJson { get; set; }
        public string ResponseType { get; set; }
    }
}
