using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.WpfConsole.Controls.Processes.ViewModels;
using Cvl.NodeNetwork.Client;

namespace Cvl.ApplicationServer.WpfConsole.Logics
{
    public class ProcessEngineClient
    {
        private string endpoint = "https://localhost:44361";
        public List<ProcessDescritpionViewModel> GetAllProcesses()
        {
            using (var factory = new ChannelFactory<IProcessEngine>(endpoint))
            {
                var serviceProxy = factory.CreateChannel();
                var processes = serviceProxy.GetAllProcessesDescriptions();
                return processes
                    .Select(x => new ProcessDescritpionViewModel(x))
                    .ToList();
            }
        }

        internal ProcessViewModel GetProcess(long id)
        {
            using (var factory = new ChannelFactory<IProcessEngine>(endpoint))
            {
                var serviceProxy = factory.CreateChannel();
                var process = serviceProxy.GetProcess(id);
                return new ProcessViewModel(process);
            }
        }

        internal void SetProcessData(FormModel formModel)
        {
            using (var factory = new ChannelFactory<IProcessEngine>(endpoint))
            {
                var serviceProxy = factory.CreateChannel();
                serviceProxy.SetProcessData(formModel);
            }
        }
    }
}
