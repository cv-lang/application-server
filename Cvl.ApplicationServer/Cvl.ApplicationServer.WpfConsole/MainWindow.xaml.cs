using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.WpfConsole.ViewModels;
using Cvl.NodeNetwork.Client;
using Cvl.NodeNetwork.Test;
using Telerik.Windows.Controls;


//using Cvl.NodeNetwork.Client;

namespace Cvl.ApplicationServer.WpfConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new VisualStudio2013Theme();

            InitializeComponent();

            ViewModel = new ConsoleViewModel();
            DataContext = ViewModel;
        }

        public ConsoleViewModel ViewModel { get; set; }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Load();


            
        }
    }
}
