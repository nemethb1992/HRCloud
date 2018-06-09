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
using static HRCloud.Model.szakmai_m;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Szakmai_panel.xaml
    /// </summary>
    public partial class Szakmai_panel : UserControl
    {
        private Grid grid;
        projekt_cont p_control = new projekt_cont();
        szakmai_cont sz_control = new szakmai_cont();
        private SzakmaiProjekt_DataView szDataView;
        private Szakmai_Kezdolap szakmai_Kezdolap;
        public Szakmai_panel(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            SettingUp();
        }
        private void Projekt_Open_btn(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Projekt_Bevont_struct items = button.DataContext as Projekt_Bevont_struct;
            p_control.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szDataView = new SzakmaiProjekt_DataView(grid));
        }
        void SettingUp()
        {
            bevont_projekt_lista.ItemsSource = sz_control.bevont_projekt_DataSource();
        }
        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Kezdolap = new Szakmai_Kezdolap(grid));
        }
    }
}
