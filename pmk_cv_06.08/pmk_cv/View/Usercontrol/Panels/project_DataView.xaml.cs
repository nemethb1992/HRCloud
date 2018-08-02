using HRCloud.Control;
using HRCloud.Model;
using HRCloud.Public.templates;
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

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_DataView.xaml
    /// </summary>
    public partial class project_DataView : UserControl
    {

        projekt_cont pcontrol = new projekt_cont();
        megjegyzes_cs comment = new megjegyzes_cs();
        applicant_cont acontrol = new applicant_cont();
        email_template et = new email_template();
        email_cont email = new email_cont();
        projekt_applicant_cont pa_control = new projekt_applicant_cont();
        Session sess = new Session();
        private Grid grid;
        private projekt_jelolt_DataView projekt_jelolt_DataView;
        private project_panel projekt_Panel;
        public project_DataView(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            try
            {
                FormLoader();
            }
            catch (Exception)
            {
                grid.Children.Clear();
                grid.Children.Add(projekt_Panel = new project_panel(grid));
            }
        }
        void FormLoader()
        {
            List<ProjectExtendedListItems> li = pcontrol.ProjektFullDataSource();
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
                if(item.id == li[0].kepesseg1)
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

            feladatok_tbx.Text = li[0].feladatok;
            elvarasok_tbx.Text = li[0].elvarasok;
            kinalunk_tbx.Text = li[0].kinalunk;
            elonyok_tbx.Text = li[0].elonyok;
            Sum_koltsegek();

            if(li[0].publikalt == 1)
            {
                publikalt_check.IsChecked = true;
            }

            //applicant_cbx.ItemsSource = pcontrol.JeloltDataSource_cbx();
            //hr_cbx.ItemsSource = pcontrol.HrDataSource_cbx();
            //felelos_cbx.ItemsSource = pcontrol.ErtesitendokDataSource_cbx();

            KisListaTolto();

            //csatolmany_listBox.ItemsSource = pcontrol.CsatolmanyDataSource();

        }
        private void KisListaTolto()
        {
            megjegyzes_listBox.ItemsSource = pcontrol.megjegyzes_datasource();
            kapcs_jeloltek_listBox.ItemsSource = pcontrol.JeloltListSourceForListBox();
            kapcs_ertesitendo_listBox.ItemsSource = pcontrol.ErtesitendokDataSource_toProjektList();
            kapcs_hr_listBox.ItemsSource = pcontrol.HrDataSource_toProjektList();
            koltseg_listBox.ItemsSource = pcontrol.Projekt_Koltseg_Query();
        }

        private void tovabb_jeloltre_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;
            acontrol.ApplicantID = items.id;
            if(items.allapota >= 1)
            {
                pa_control.TelefonSzurt = 1;
            }
            else
            {
                pa_control.TelefonSzurt = 0;
            }
            grid.Children.Clear();
            grid.Children.Add(projekt_jelolt_DataView = new projekt_jelolt_DataView(grid));
        }



        private void jeloltek_tabbtn_Click(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Visible;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Hidden;

            jeloltek_addbtn.Visibility = System.Windows.Visibility.Visible;
            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Hidden;
            hr_addbtn.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void ertesitendok_tabbtn_Click(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Visible;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Hidden;

            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Visible;
            jeloltek_addbtn.Visibility = System.Windows.Visibility.Hidden;
            hr_addbtn.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void hr_tabbtn_Click(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Visible;

            hr_addbtn.Visibility = System.Windows.Visibility.Visible;
            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Hidden;
            jeloltek_addbtn.Visibility = System.Windows.Visibility.Hidden;
            
        }

        //private void add_applicant_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = applicant_cbx as ComboBox;
        //    JeloltListItems jelolt = cbx.SelectedItem as JeloltListItems;
        //    pcontrol.Jelolt_write_to_project(jelolt.id);
        //    FormLoader();
        //}

        //private void add_hr_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = hr_cbx as ComboBox;
        //    hr_struct items = cbx.SelectedItem as hr_struct;
        //    pcontrol.HR_write_to_project(items.id);
        //    KisListaTolto(megjegyzes_listBox);
        //}

        //private void add_felelos_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = felelos_cbx as ComboBox;
        //    ertesitendok_struct items = cbx.SelectedItem as ertesitendok_struct;
        //    pcontrol.Ertesitendok_write_to_project(items.id);
        //    KisListaTolto(megjegyzes_listBox);
        //}

        private void jelolt_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image delete = sender as Image;
            JeloltListItems items = delete.DataContext as JeloltListItems;
            pcontrol.Jelolt_list_delete(items.id);
            kapcs_jeloltek_listBox.ItemsSource = pcontrol.JeloltListSourceForListBox();
        }

        private void ertesitendo_del_Click(object sender, RoutedEventArgs e)
        {
            Button delete = sender as Button;
            ertesitendok_struct items = delete.DataContext as ertesitendok_struct;
            pcontrol.Ertesitendok_list_delete(items.id);
            kapcs_ertesitendo_listBox.ItemsSource = pcontrol.ErtesitendokDataSource_toProjektList();
        }

        private void hr_del_Click(object sender, RoutedEventArgs e)
        {
            Button delete = sender as Button;
            hr_struct items = delete.DataContext as hr_struct;
            pcontrol.HR_list_delete(items.id);
            kapcs_hr_listBox.ItemsSource = pcontrol.HrDataSource_toProjektList();
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;
            comment.megjegyzes_torles(items.id, sess.UserData[0].id, pcontrol.ProjektID, 0);
            KisListaTolto();
        }

        private void Jelolt_Context(object sender, RoutedEventArgs e)
        {
            MenuItem mitem = sender as MenuItem;
            JeloltListItems items = mitem.DataContext as JeloltListItems;
            if (mitem.Tag.ToString() == "delete")
            {
                MessageBoxResult result = MessageBox.Show("Elutasító E-Mail kiküldésre kerüljön?", "My App", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        pcontrol.Jelolt_list_delete(items.id);
                        email.Mail_Send(items.email, et.Elutasito_Email(items.nev));
                        break;
                    case MessageBoxResult.No:
                        pcontrol.Jelolt_list_delete(items.id);
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                pcontrol.Jelolt_list_allpot_UPDATE(items.id, Convert.ToInt32(mitem.Tag));
            }

            KisListaTolto();
            FormLoader();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.megjegyzes_feltoltes(comment_tartalom.Text, pcontrol.ProjektID, 0,0);
            KisListaTolto();
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
        private void Ember_Hozzaadas_Megse(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Hidden;
            FormLoader();
        }
        private static int Kod;
        public int kod { get { return Kod; } set { Kod = value; } }
        private void jeloltek_addbtn_Click(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Visible;
            kod = 1;
            Ember_Search_Listbox.ItemsSource = pcontrol.JeloltDataSource_cbx(Ember_Search_tbx.Text);
            FormLoader();
        }

        private void ertesitendok_addbtn_Click(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Visible;
            kod = 2;
            Ember_Search_Listbox.ItemsSource = pcontrol.ErtesitendokDataSource_cbx(Ember_Search_tbx.Text);
        }

        private void hr_addbtn_Click(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Visible;
            kod = 3;
            Ember_Search_Listbox.ItemsSource = pcontrol.HrDataSource_cbx(Ember_Search_tbx.Text);
        }

        private void Ember_Add_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(kod == 1)
            {
                SubJelolt items;
                items = btn.DataContext as SubJelolt;
                pcontrol.Jelolt_write_to_project(items.id, pcontrol.ProjektID);
                Ember_Search_Listbox.ItemsSource = pcontrol.JeloltDataSource_cbx(Ember_Search_tbx.Text);
            }
            if (kod == 2)
            {
                ertesitendok_struct items;
                items = btn.DataContext as ertesitendok_struct;
                pcontrol.Ertesitendok_write_to_project(items.id);
                Ember_Search_Listbox.ItemsSource = pcontrol.ErtesitendokDataSource_cbx(Ember_Search_tbx.Text);
            }
            if (kod == 3)
            {
                hr_struct items;
                items = btn.DataContext as hr_struct;
                pcontrol.HR_write_to_project(items.id);
                Ember_Search_Listbox.ItemsSource = pcontrol.HrDataSource_cbx(Ember_Search_tbx.Text);
            }
            
        }

        private void Ember_Search_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Ember_Search_Listbox.ItemsSource = pcontrol.JeloltDataSource_cbx(Ember_Search_tbx.Text);
        }

        private void Projekt_Leiras_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            string type = tbx.Tag.ToString();
            pcontrol.Projekt_Leiras_Update(type, tbx.Text);
            FormLoader();
        }
        private void Sum_koltsegek()
        {
            int sum = 0;
            List<koltsegek> list = pcontrol.Projekt_Koltseg_Query();
            foreach (var item in list)
            {
                sum += item.osszeg;
            }
            ossz_koltseg.Text = sum.ToString()+" ft";
        }

        private void Koltseg_mentes(object sender, RoutedEventArgs e)
        {
            pcontrol.Projekt_Koltseg_Insert(k_megnevezes_tbx.Text, k_osszeg_tbx.Text);
            koltseg_listBox.ItemsSource = pcontrol.Projekt_Koltseg_Query();
            Sum_koltsegek();
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
        }

        private void Koltse_megsem(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
            k_megnevezes_tbx.Text = "";
            k_osszeg_tbx.Text = "";
        }
        private void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void koltseg_open_btn_Click(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Visible;
        }

        private void Koltseg_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            koltsegek item = menu.DataContext as koltsegek;
            pcontrol.Projekt_Koltseg_Delete(item.id);
            koltseg_listBox.ItemsSource = pcontrol.Projekt_Koltseg_Query();
            Sum_koltsegek();
        }

        private void publikalt_check_Checked(object sender, RoutedEventArgs e)
        {
            pcontrol.Projekt_publikal(1);
        }

        private void publikalt_check_Unchecked(object sender, RoutedEventArgs e)
        {
            pcontrol.Projekt_publikal(0);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
                Grid grid = sender as Grid;
            JeloltListItems item = grid.DataContext as JeloltListItems;
                if (item.Checked == false)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
                kapcs_jeloltek_listBox.Items.Refresh();
        }
    }
}
