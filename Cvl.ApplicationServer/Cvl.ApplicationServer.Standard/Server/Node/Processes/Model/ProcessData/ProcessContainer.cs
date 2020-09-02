using Cvl.ApplicationServer.Base.Model;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Obiekt przechowujący proces i wirutalną maszynę na której się wykonuje proces
    /// </summary>
    public class ProcessContainer : BaseObject
    {
        public BaseProcess Process { get; internal set; }
        public VirtualMachine.VirtualMachine VirtualMachine { get; internal set; }
    }
}
