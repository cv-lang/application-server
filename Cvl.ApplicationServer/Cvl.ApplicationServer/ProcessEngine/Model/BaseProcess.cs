using Cvl.ApplicationServer.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.ProcessEngine.Model
{
    public class BaseProcess : BaseObject
    {
        public EnumProcessStatus ProcessStatus { get; set; }
    }
}
