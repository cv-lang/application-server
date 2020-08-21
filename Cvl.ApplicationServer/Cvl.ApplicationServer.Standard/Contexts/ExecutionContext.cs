using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Contexts.Application;

namespace Cvl.ApplicationServer.Contexts
{
    
    public class ExecutionContext
    {
        public string UserName { get; set; }
        public ApplicationContext ApplicationContext { get; internal set; }
    }
}
