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
    public partial class Szakmai_Kezdolap : UserControl
    {
        private Grid grid;
        projekt_cont p_control = new projekt_cont();
        szakmai_cont sz_control = new szakmai_cont();
        private Szakmai_panel szakmai_Panel;
        private Szakmai_interju_lista szakmai_Interju_Lista;
        public Szakmai_Kezdolap(Grid grid)
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
            grid.Children.Add(szakmai_Panel = new Szakmai_panel(grid));
        }

        private void Szakmai_interjuk_Button(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Interju_Lista = new Szakmai_interju_lista(grid));
        }
    }
}
