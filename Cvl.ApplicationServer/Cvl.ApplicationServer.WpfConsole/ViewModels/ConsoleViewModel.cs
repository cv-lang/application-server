using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.WpfConsole.Controls.OfflineProcesses.ViewModels;
using Cvl.ApplicationServer.WpfConsole.Controls.Processes.ViewModels;

namespace Cvl.ApplicationServer.WpfConsole.ViewModels
{
    public class ConsoleViewModel : ViewModels.Base.ViewModelBase
    {
        public ConsoleViewModel()
        {
            this.ProcessesList = new ProcessesListViewModel(this);
            OfflineProcess = new OfflineProcessViewModel(this);
        }

        private ProcessesListViewModel processesList;
        public ProcessesListViewModel ProcessesList
        {
            set
            {
                processesList = value;
                OnPropertyChanged();
            }
            get { return processesList; }
        }

        public OfflineProcessViewModel OfflineProcess { get; set; }

        internal void Load()
        {
            try
            {
                ProcessesList.Load();
            }
            catch { }
        }
    }
}
