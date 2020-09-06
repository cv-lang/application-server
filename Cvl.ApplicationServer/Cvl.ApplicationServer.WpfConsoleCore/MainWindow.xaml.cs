using Cvl.ApplicationServer.WpfConsole.ViewModels;
using System;
using System.Collections.Generic;
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
using Telerik.Windows.Controls;

namespace Cvl.ApplicationServer.WpfConsoleCore
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