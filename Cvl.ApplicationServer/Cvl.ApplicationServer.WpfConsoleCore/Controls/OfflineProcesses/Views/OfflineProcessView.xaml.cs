using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Telerik.Windows.Controls.Data.PropertyGrid;
using Cvl.ApplicationServer.Tools.Extension;

namespace Cvl.ApplicationServer.WpfConsole.Controls.OfflineProcesses
{
    /// <summary>
    /// Logika interakcji dla klasy OfflineProcessView.xaml
    /// </summary>
    public partial class OfflineProcessView : UserControl
    {
        public OfflineProcessView()
        {
            InitializeComponent();
        }

        private void RadPropertyGrid_OnAutoGeneratingPropertyDefinition(object sender, AutoGeneratingPropertyDefinitionEventArgs e)
        {
            var propertyGrid = sender as RadPropertyGrid;
            var property = e.PropertyDefinition.SourceProperty;
            var item = propertyGrid.Item;


            var atrybuty = ((System.ComponentModel.MemberDescriptor)(property.Descriptor)).Attributes;
            var atrybutyTypowane = atrybuty.Cast<Attribute>();

            var pi = item.GetType().GetProperty(property.Name);
            if (pi != null)
            {
                e.PropertyDefinition.Description = pi.GetPropertyDescription();
                e.PropertyDefinition.GroupName = pi.GetPropertyGroup();
            }
            else
            {
                e.PropertyDefinition.Description = e.PropertyDefinition.PropertyDescriptor.Description;
            }
        }
    }
}
