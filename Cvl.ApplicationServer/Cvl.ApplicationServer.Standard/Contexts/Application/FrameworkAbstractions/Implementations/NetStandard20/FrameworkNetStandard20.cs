using Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.AbstractionElements;
using Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.Implementations.NetStandard20;
using Cvl.ApplicationServer.Contexts.FrameworkAbstractions.AbstractionElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.FrameworkAbstractions.Implementations.NetStandard20
{
    public class FrameworkNetStandard20 : IFrameworkAbstraction
    {
        public FrameworkNetStandard20()
        {
        }
        
        public IConfigurationAbstraction Configuration => new ConfigurationNetStandard20(this);
        public IIOAbstraction IO => new IONetStandard20();
    }
}
