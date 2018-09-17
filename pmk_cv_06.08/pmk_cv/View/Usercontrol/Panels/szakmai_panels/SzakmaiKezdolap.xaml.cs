using HRCloud.Control;
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

namespace HRCloud.View.Usercontrol.Panels.szakmai_panels
{
    /// <summary>
    /// Interaction logic for Szakmai_Kezdolap.xaml
    /// </summary>
    public partial class SzakmaiKezdolap : UserControl
    {
        private Grid grid;
        ControlProject p_control = new ControlProject();
        ControlSzakmai sz_control = new ControlSzakmai();
        private SzakmaiList szakmai_Panel;
        private SzakmaiInterviewList szakmai_Interju_Lista;
        public SzakmaiKezdolap(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            StartUp();
        }
        void StartUp()
        {
            interju_no.Text = sz_control.bevont_interju_DataSource().Count.ToString() + " db";
            projekt_no.Text = sz_control.bevont_projekt_DataSource().Count.ToString() + " db";
        }

        private void Szakmai_panel_Button(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Panel = new SzakmaiList(grid));
        }

        private void Szakmai_interjuk_Button(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Interju_Lista = new SzakmaiInterviewList(grid));
        }
    }
}
