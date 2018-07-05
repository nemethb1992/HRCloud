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

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for interju_panel.xaml
    /// </summary>
    public partial class interju_panel : UserControl
    {
        private Grid grid;
        private projekt_jelolt_DataView projekt_Jelolt;
        private Szakmai_interju_lista szakmai_Interju_Lista;

        applicant_cont a_control = new applicant_cont();
        projekt_cont p_control = new projekt_cont();
        Session sess = new Session();
        projekt_applicant_cont pa_control = new projekt_applicant_cont();
        public interju_panel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            FormLoadUp();
            if (sess.UserData[0].kategoria == 0)
            {
                add_meghivott.Visibility = Visibility.Hidden;
            }
        }

        private void Back_To_pj_button(object sender, RoutedEventArgs e)
        {
            if(sess.UserData[0].kategoria == 1)
            {
                grid.Children.Clear();
                grid.Children.Add(projekt_Jelolt = new projekt_jelolt_DataView(grid));
            }
            else
            {
                grid.Children.Clear();
                grid.Children.Add(szakmai_Interju_Lista = new Szakmai_interju_lista(grid));
            }
        }
        private void FormLoadUp()
        {
            List<interju_struct> list = pa_control.Interju_DataSource();
            List<ProjectExtendedListItems> li = p_control.ProjektFullDataSource();
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
            interju_jelolt_tbl.Text = list[0].jelolt_megnevezes;
            interju_projekt_tbl.Text = list[0].projekt_megnevezes;
            interju_cim_tbl.Text = list[0].interju_cim;
            interju_helye_tbl.Text = list[0].helyszin;
            interju_liras_tbl.Text = list[0].interju_leiras;

            choose_editlist.ItemsSource = pa_control.bevon_ertesitendok_DataSource();
            ertesitendok_editlist.ItemsSource = pa_control.interjuhoz_adott_ertesitendok_DataSource();

            if (pa_control.Kompetencia_valider())
            {
                Panel.SetZIndex(kompetencia_border, 1);
                locked_title.Visibility = Visibility.Visible;
                teszt_nyitas_btn.Visibility = Visibility.Visible;
            }
        }

        private void kompetencia_send_btn(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int type = Convert.ToInt32(btn.Tag);
            List<ProjectExtendedListItems> li = p_control.ProjektFullDataSource();

            List<int> list = new List<int>() ;
            list.Add(li[0].kepesseg1);
            list.Add(Convert.ToInt32(k1_slider.Value));
            list.Add(li[0].kepesseg2);
            list.Add(Convert.ToInt32(k2_slider.Value));
            list.Add(li[0].kepesseg3);
            list.Add(Convert.ToInt32(k3_slider.Value));
            list.Add(li[0].kepesseg4);
            list.Add(Convert.ToInt32(k4_slider.Value));
            list.Add(li[0].kepesseg5);
            list.Add(Convert.ToInt32(k5_slider.Value));
            list.Add(type);
            pa_control.Kopmetencia_ertekeles_INSERT(list);

            Panel.SetZIndex(kompetencia_border,1);
            locked_title.Visibility = Visibility.Visible;
            teszt_nyitas_btn.Visibility = Visibility.Visible;


        }
        private void add_meghivott_Click(object sender, RoutedEventArgs e)
        {
            add_meghivott.Visibility = Visibility.Hidden;
            megse_meghivott.Visibility = Visibility.Visible;
            ertesitendok_editlist.Visibility = Visibility.Hidden;
            choose_editlist.Visibility = Visibility.Visible;

            choose_editlist.ItemsSource = pa_control.bevon_ertesitendok_DataSource();
        }
        private void megsem_meghivott_Click(object sender, RoutedEventArgs e)
        {
            add_meghivott.Visibility = Visibility.Visible;
            megse_meghivott.Visibility = Visibility.Hidden;
            ertesitendok_editlist.Visibility = Visibility.Visible;
            choose_editlist.Visibility = Visibility.Hidden;

            ertesitendok_editlist.ItemsSource = pa_control.interjuhoz_adott_ertesitendok_DataSource();
        }

        private void Add_Ertesitendo_to_interju_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ertesitendok_struct items = btn.DataContext as ertesitendok_struct;
            pa_control.Write_User_To_Inerju(items.id);

            choose_editlist.ItemsSource = pa_control.bevon_ertesitendok_DataSource();
            ertesitendok_editlist.ItemsSource = pa_control.interjuhoz_adott_ertesitendok_DataSource();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if(sess.UserData[0].kategoria == 1)
            {
                MenuItem menu = sender as MenuItem;
                ertesitendok_struct items = menu.DataContext as ertesitendok_struct;
                pa_control.Delete_User_To_Inerju(items.id);
                choose_editlist.ItemsSource = pa_control.bevon_ertesitendok_DataSource();
                ertesitendok_editlist.ItemsSource = pa_control.interjuhoz_adott_ertesitendok_DataSource();
            }
        }
    }
}
