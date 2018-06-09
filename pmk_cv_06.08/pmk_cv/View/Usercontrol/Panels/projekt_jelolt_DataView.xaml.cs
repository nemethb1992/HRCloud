using HRCloud.Control;
using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static HRCloud.Control.projekt_applicant_cont;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for projekt_jelolt_DataView.xaml
    /// </summary>
    /// 
    public partial class projekt_jelolt_DataView : UserControl
    {
        private Grid grid;
        megjegyzes_cs comment = new megjegyzes_cs();
        private project_DataView project_DataView;
        private interju_panel interju_Panel;
        applicant_cont acontrol = new applicant_cont();
        Session sess = new Session();
        projekt_cont pcontrol = new projekt_cont();
        projekt_applicant_cont pa_control = new projekt_applicant_cont();
        public projekt_jelolt_DataView(Grid grid)
        {
                this.grid = grid;
                InitializeComponent();
                form_loadup();
        }

        private void form_loadup()
        {
            List<JeloltExtendedList> a_li = acontrol.JeloltFullDataSource();
            List<ProjectExtendedListItems> p_li = pcontrol.ProjektFullDataSource();
            List<kompetenciak> li_k = pa_control.kompetencia_DataSource();
            List<kompetencia_summary_struct> li_kvalue = pa_control.kompetencia_jelolt_kapcs_DataSource();
            projekt_jelolt_title_tbl.Text = p_li[0].megnevezes_projekt + " - " + a_li[0].nev;
            jelolt_telefon.Text = "( " + a_li[0].telefon + " )";
            megjegyzes_listBox.ItemsSource = pcontrol.megjegyzes_datasource_kapcsolati();
            kapcs_jeloltek_listBox.ItemsSource = pa_control.Interju_DataSource();
            try
            {
                k_1.Value = li_kvalue[0].k1_val;
                k_2.Value = li_kvalue[0].k2_val;
                k_3.Value = li_kvalue[0].k3_val;
                k_4.Value = li_kvalue[0].k4_val;
                k_5.Value = li_kvalue[0].k5_val;
            }
            catch (Exception)
            {
            }
            foreach (var item in li_k)
            {
                if (item.id == p_li[0].kepesseg1)
                { kompetencia_1.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg2)
                { kompetencia_2.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg3)
                { kompetencia_3.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg4)
                { kompetencia_4.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg5)
                { kompetencia_5.Text = item.kompetencia_megnevezes; }
            }


            telefonos_szures_declarer();
        }
        private void telefonos_szures_declarer()
        {
            if (pa_control.TelefonSzurt == 1)
            {
                telefonos_igen_btn.Visibility = Visibility.Hidden;
                szurt_tbl.Visibility = Visibility.Visible;
            }
        }
        private void Back_To_projekt_btn(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(project_DataView = new project_DataView(grid));
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.megjegyzes_feltoltes(comment_tartalom.Text, pcontrol.ProjektID, acontrol.ApplicantID, 0);
            megjegyzes_listBox.ItemsSource = pcontrol.megjegyzes_datasource_kapcsolati();
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;
            comment.megjegyzes_torles(items.id, sess.UserData[0].id, pcontrol.ProjektID, acontrol.ApplicantID);
            megjegyzes_listBox.ItemsSource = pcontrol.megjegyzes_datasource_kapcsolati();
        }

        private void Telefon_Megnyitas_btn_Click(object sender, RoutedEventArgs e)
        {
            ismerte_cbx.Items.Add("nem");
            ismerte_cbx.Items.Add("igen");
            ismerte_cbx.SelectedIndex = 0;
            grid_telefonosszuro.Height = 400;
            telefonos_igen_btn.IsEnabled = false;
            telefonos_nem_btn.IsEnabled = false;
        }
        private void Telefon_Osszecsukas_btn_Click(object sender, RoutedEventArgs e)
        {
            grid_telefonosszuro.Height = 100;
            ismerte_cbx.Items.Clear();
            muszakok_tbx.Text = "";
            utazas_tbx.Text = "";
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
        }

        private void Telefon_Elfogadas_btn_Click(object sender, RoutedEventArgs e)
        {
            int ismerte = 0;
            if(ismerte_cbx.SelectedItem == "igen")
            {
                ismerte = 1;
            }
            pa_control.Telefon_Szures_Elfogad(ismerte,Convert.ToInt32(muszakok_tbx.Text),utazas_tbx.Text);
            pa_control.TelefonSzurt = 1;
            grid_telefonosszuro.Height = 100;
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
            telefonos_szures_declarer();
        }
        private void Telefon_Szures_Elutasit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan elutasítja? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    pa_control.Telefon_Szures_Elutasit();
                    grid.Children.Clear();
                    grid.Children.Add(project_DataView = new project_DataView(grid));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        void Interju_panel_open()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");
            grid.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
        }
        void Interju_panel_close()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");
            grid.Visibility = Visibility.Hidden;
            grid2.Visibility = Visibility.Hidden;
        }
        private void Uj_Interju_felvetele_Click(object sender, RoutedEventArgs e)
        {
            Interju_panel_open();
        }

        private void uj_interju_megsem_btn_Click(object sender, RoutedEventArgs e)
        {
            Interju_panel_close();
        }

        private void ui_bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Interju_panel_close();
        }

        private void uj_interju_mentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] seged = inter_date.SelectedDate.ToString().Split(' ');
            string datum = seged[0] + seged[1] + seged[2];
            try
            {
                pa_control.Insert_interju(datum, inter_cim.Text, inter_leiras.Text, inter_helyszin.Text);
                form_loadup();
                Interju_panel_close();
            }
            catch (Exception)
            {
                MessageBox.Show("Nincs kitöltve minden mező!");
            }
        }

        private void Interju_Datapanel_open(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            interju_struct items = btn.DataContext as interju_struct;
            pa_control.InterjuID = items.id;
            grid.Children.Clear();
            grid.Children.Add(interju_Panel = new interju_panel(grid));
        }

        private void Interju_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            interju_struct items = menu.DataContext as interju_struct;
            pa_control.interju_delete(items.id);
            form_loadup();
        }

        private void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }




    }

}
