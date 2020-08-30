using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.WpfConsole.Logics;
using Cvl.ApplicationServer.WpfConsole.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Cvl.ApplicationServer.WpfConsole.Controls.OfflineProcesses.ViewModels
{
    public class OfflineProcessViewModel : ViewModelBase
    {
        public OfflineProcessViewModel(WpfConsole.ViewModels.ConsoleViewModel consoleViewModel)
        {
            OpenProcess = new RelayCommand(openProcess);
            NextStep = new RelayCommand(nextStep);

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        

        private ProcessEngineClient processEngineClient = new ProcessEngineClient();

        private long processId;
        public long ProcessId
        {
            set
            {
                processId = value;
                OnPropertyChanged();
            }
            get { return processId; }
        }

        private Visibility waitingVisiblity = Visibility.Visible;
        public Visibility WaitingVisiblity
        {
            set
            {
                waitingVisiblity = value;
                OnPropertyChanged();
            }
            get { return waitingVisiblity; }
        }

        private Visibility formVisiblity = Visibility.Collapsed;
        public Visibility FormVisiblity
        {
            set
            {
                formVisiblity = value;
                OnPropertyChanged();
            }
            get { return formVisiblity; }
        }

        private BaseModel formModel;
        public BaseModel FormModel
        {
            set
            {
                formModel = value;
                OnPropertyChanged();
            }
            get { return formModel; }
        }

        public RelayCommand OpenProcess { get; set; }
        private void openProcess()
        {
            var processData = processEngineClient.GetProcess(ProcessId);
            if (processData.Process.ProcessStatus == Server.Node.Processes.Model.EnumProcessStatus.WaitingForUserData)
            {
                WaitingVisiblity = Visibility.Collapsed;
                FormVisiblity = Visibility.Visible;
                FormModel = processData.Process.ProcessUI.FormDataToShow.FormDataModel;
                timer.Stop();
            }
            else
            {
                WaitingVisiblity = Visibility.Visible;
                FormVisiblity = Visibility.Collapsed;
            }            
        }

        

        public RelayCommand NextStep { get; set; }
        private void nextStep()
        {
            FormModel.ClientIpAddress = "offline";
            processEngineClient.SetProcessData(FormModel);
            timer.Start();            
        }

        DispatcherTimer timer = new DispatcherTimer();
        private void Timer_Tick(object sender, EventArgs e)
        {
            openProcess();            
        }
    }
}
