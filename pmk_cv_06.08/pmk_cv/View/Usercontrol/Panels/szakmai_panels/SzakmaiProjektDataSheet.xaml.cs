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
    /// Interaction logic for SzakmaiProjekt_DataView.xaml
    /// </summary>
    public partial class SzakmaiProjektDataSheet : UserControl
    {

        private Grid grid;
        private SzakmaiList szakmai_Panel;
        //private applicant_DataView Applicant_DataView;
        private SzakmaiApplicantDataView szakmai_Applicant_Data;
        ControlProject p_control = new ControlProject();
        ControlApplicant a_control = new ControlApplicant();
        ControlApplicantProject pa_control = new ControlApplicantProject();
        Session sess = new Session();
        Comment comment = new Comment();

        public SzakmaiProjektDataSheet(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            FormLoader();
        }
        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Panel = new SzakmaiList(grid));
        }
        void FormLoader()
        {
            List<ProjectExtendedListItems> li = p_control.ProjektFullDataSource();
            projekt_profile_title.Text = li[0].megnevezes_projekt;
            projekt_input_1.Text = li[0].statusz.ToString();
            projekt_input_2.Text = li[0].megnevezes_munka;
            projekt_input_3.Text = li[0].megnevezes_pc;
            projekt_input_4.Text = li[0].megnevezes_vegzettseg;
            projekt_input_5.Text = li[0].megnevezes_nyelv;
            projekt_input_6.Text = li[0].jeloltek_db.ToString();
            projekt_input_7.Text = li[0].megnevezes_hr;
            projekt_input_8.Text = li[0].fel_datum.ToString();
            projekt_input_9.Text = li[0].ber.ToString() + " Ft";
            projekt_input_10.Text = li[0].tapasztalat_ev.ToString();

            List<kompetenciak> li_k = pa_control.kompetencia_DataSource();
            foreach (var item in li_k)
            {
                if (item.id == li[0].kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            megjegyzes_listBox.ItemsSource = p_control.megjegyzes_datasource();
            kapcs_jeloltek_listBox.ItemsSource = p_control.JeloltListSourceForListBox();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;
            comment.megjegyzes_torles(items.id, sess.UserData[0].id, p_control.ProjektID, 0);
            megjegyzes_listBox.ItemsSource = p_control.megjegyzes_datasource();
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.megjegyzes_feltoltes(comment_tartalom.Text, p_control.ProjektID, 0, 0);
            megjegyzes_listBox.ItemsSource = p_control.megjegyzes_datasource();
            tbx.Text = "";
        }

        private void comment_tartalom_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Text == "Új megjegyzés")
            {
                tbx.Text = "";
            }
        }
        private void comment_tartalom_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Text == "")
            {
                tbx.Text = "Új megjegyzés";
            }
        }
        private void tovabb_jeloltre_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;
            a_control.ApplicantID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmai_Applicant_Data = new SzakmaiApplicantDataView(grid));
        }
    }
}
