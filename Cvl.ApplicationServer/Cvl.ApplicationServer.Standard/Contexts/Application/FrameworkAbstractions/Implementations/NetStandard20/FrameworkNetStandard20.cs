using Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.AbstractionElements;

namespace Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.Implementations.NetStandard20
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
