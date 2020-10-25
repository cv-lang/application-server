using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private string endpoint = "https://localhost:44369";
        public List<ProcessDescritpionViewModel> GetAllProcesses()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyLoad += new AssemblyLoadEventHandler(MyAssemblyLoadEventHandler);
            //currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


            

            using (var factory = new ChannelFactory<IProcessEngine>(endpoint))
            {
                var serviceProxy = factory.CreateChannel();
                var processes = serviceProxy.GetAllProcessesDescriptions();
                return processes
                    .Select(x => new ProcessDescritpionViewModel(x))
                    .ToList();
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var t = args.Name.Split(',').First();
                var assemblyPath = $"{path}{t}.dll";
                var a = Assembly.LoadFile(assemblyPath);
                return a;
            }
            catch (Exception ex)
            {

            }
            return Assembly.LoadFrom(args.Name);

            
        }

        static void MyAssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("ASSEMBLY LOADED: " + args.LoadedAssembly.FullName);
            Console.WriteLine();
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

        internal void SetProcessData(FormViewModel formModel)
        {
            using (var factory = new ChannelFactory<IProcessEngine>(endpoint))
            {
                var serviceProxy = factory.CreateChannel();
                serviceProxy.SetProcessData(formModel);
            }
        }
    }
}
