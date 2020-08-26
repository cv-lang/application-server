using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Monitoring.Base.Model;
using Cvl.ApplicationServer.WpfConsole.Logics;
using Cvl.ApplicationServer.WpfConsole.ViewModels;
using Cvl.ApplicationServer.WpfConsole.ViewModels.Base;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Persistence.Core;

namespace Cvl.ApplicationServer.WpfConsole.Controls.Processes.ViewModels
{
    public class ProcessesListViewModel : ViewModelBase
    {
        private ConsoleViewModel consoleViewModel;
        private ProcessEngineClient processEngineClient = new ProcessEngineClient();

        public ProcessesListViewModel(ConsoleViewModel consoleViewModel)
        {
            this.consoleViewModel = consoleViewModel;
            LoadCommand = new RelayCommand(Load);
        }

        public ObservableCollection<ProcessDescritpionViewModel> Processes { get; set; }
            = new ObservableCollection<ProcessDescritpionViewModel>();

        private ProcessDescritpionViewModel selectedProcessDescription;
        public ProcessDescritpionViewModel SelectedProcessDescritions
        {
            set
            {
                selectedProcessDescription = value;
                if (selectedProcessDescription != null)
                {
                    SelectedProcess = processEngineClient.GetProcess(selectedProcessDescription.ProcessDescription.Id);
                    Logs.Clear();
                    Logs.AddRange(SelectedProcess.Process.Logger.Logs);
                }

                OnPropertyChanged();
            }
            get { return selectedProcessDescription; }
        }


        private ProcessViewModel selectedProces;
        public ProcessViewModel SelectedProcess
        {
            set
            {
                selectedProces = value;
                OnPropertyChanged();
            }
            get { return selectedProces; }
        }


        #region Logs

        private ObservableCollection<LogModel> logs =new ObservableCollection<LogModel>();
        public ObservableCollection<LogModel> Logs
        {
            set
            {
                logs = value;
                OnPropertyChanged();
            }
            get { return logs; }
        } 

        private LogModel selectedLog;
        public LogModel SelectedLog
        {
            set
            {
                selectedLog = value;
                OnPropertyChanged();
            }
            get { return selectedLog; }
        }

        #endregion


        public RelayCommand LoadCommand { get; set; } 

        internal void Load()
        {
            Processes.Clear();
            Processes.AddRange(processEngineClient.GetAllProcesses());
        }
    }
}
