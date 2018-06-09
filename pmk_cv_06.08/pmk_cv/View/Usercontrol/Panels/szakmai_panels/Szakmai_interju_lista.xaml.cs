using HRCloud.Control;
using HRCloud.Model;
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
    /// Interaction logic for Szakmai_interju_lista.xaml
    /// </summary>
    public partial class Szakmai_interju_lista : UserControl
    {
        private Grid grid;
        private Szakmai_Kezdolap szakmai_Kezdolap;
        private interju_panel interju_Panel;
        projekt_applicant_cont pa_control = new projekt_applicant_cont();
        projekt_cont p_control = new projekt_cont();
        applicant_cont a_control = new applicant_cont();
        szakmai_cont sz_control = new szakmai_cont();


        public Szakmai_interju_lista(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            bevont_interjuk_listbox.ItemsSource = sz_control.bevont_interju_DataSource();
        }
        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Kezdolap = new Szakmai_Kezdolap(grid));
        }

        private void Szakmai_interju_open_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            interju_struct items = btn.DataContext as interju_struct;
            pa_control.InterjuID = items.id;
            p_control.ProjektID = items.projekt_id;
            a_control.ApplicantID = items.jelolt_id;
            grid.Children.Clear();
            grid.Children.Add(interju_Panel = new interju_panel(grid));
        }
    }
}
