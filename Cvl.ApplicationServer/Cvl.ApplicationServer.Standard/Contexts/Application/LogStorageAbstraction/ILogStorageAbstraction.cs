using Cvl.ApplicationServer.Monitoring.Base;

namespace Cvl.ApplicationServer.Contexts.Application.LogStorageAbstraction
{
    public interface ILogStorageAbstraction
    {
        void FlushLogger(Logger logger);
    }
}
