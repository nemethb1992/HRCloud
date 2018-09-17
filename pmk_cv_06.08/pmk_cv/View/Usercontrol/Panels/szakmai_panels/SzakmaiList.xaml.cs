using HRCloud.Control;
using HRCloud.Model;
using HRCloud.View.Usercontrol.Panels.szakmai_panels;
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
using static HRCloud.Model.ModelSzakmai;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Szakmai_panel.xaml
    /// </summary>
    public partial class SzakmaiList : UserControl
    {
        ControlProject pControl = new ControlProject();
        ControlSzakmai szControl = new ControlSzakmai();

        private SzakmaiProjektDataSheet szakmaiProjektDataSheet;
        private SzakmaiKezdolap szakmaiKezdolap;
        private Grid grid;

        public SzakmaiList(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            settingUp();
        }

        protected void openProject(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Projekt_Bevont_struct items = button.DataContext as Projekt_Bevont_struct;

            pControl.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmaiProjektDataSheet = new SzakmaiProjektDataSheet(grid));
        }

        protected void settingUp()
        {
            bevont_projekt_lista.ItemsSource = szControl.Data_SzakmaiProject();
        }

        protected void navigateToSzakmaiKezdolap(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiKezdolap = new SzakmaiKezdolap(grid));
        }
    }
}
