using HRCloud.Control;
using HRCloud.Model;
using HRCloud.Public.templates;
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
    public partial class InterviewPanel : UserControl
    {
        private Grid grid;
        private ProjektJeloltDataSheet projekt_Jelolt;
        private SzakmaiInterviewList szakmai_Interju_Lista;

        ControlApplicant a_control = new ControlApplicant();
        ControlProject p_control = new ControlProject();
        ControlApplicantProject pa_control = new ControlApplicantProject();
        Session sess = new Session();
    
        public InterviewPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            interviewLoader();
            if (sess.UserData[0].kategoria == 0)
            {
                addPerson.Visibility = Visibility.Hidden;
                invitePerson.Visibility = Visibility.Hidden;
            }
        }

        protected void navigateBackFromInterview(object sender, RoutedEventArgs e)
        {
            if(sess.UserData[0].kategoria == 1)
            {
                grid.Children.Clear();
                grid.Children.Add(projekt_Jelolt = new ProjektJeloltDataSheet(grid));
            }
            else
            {
                grid.Children.Clear();
                grid.Children.Add(szakmai_Interju_Lista = new SzakmaiInterviewList(grid));
            }
        }
        protected void interviewLoader()
        {
            List<interju_struct> list = pa_control.Interju_DataSource_ByID();
            List<ProjectExtendedListItems> li = p_control.Data_ProjectFull();
            List<kompetenciak> li_k = pa_control.Data_Kompetencia();

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
            interju_idopont_tbl.Text = list[0].interju_datum +" - "+ list[0].idopont;
            interju_liras_tbl.Text = list[0].interju_leiras;

            choose_editlist.ItemsSource = pa_control.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = pa_control.Data_InterjuErtesitendokKapcsolt();

            if (pa_control.Kompetencia_valider())
            {
                Panel.SetZIndex(kompetencia_border, 1);
                locked_title.Visibility = Visibility.Visible;
                teszt_nyitas_btn.Visibility = Visibility.Visible;
            }
        }

        protected void kompetencia_send_btn(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int type = Convert.ToInt32(button.Tag);
            List<ProjectExtendedListItems> li = p_control.Data_ProjectFull();

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

            pa_control.kompetenciaUpdate(list);
            Panel.SetZIndex(kompetencia_border,1);
            locked_title.Visibility = Visibility.Visible;
            teszt_nyitas_btn.Visibility = Visibility.Visible;


        }
        protected void openColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Visible;
            Blur_Grid.Visibility = Visibility.Visible;
            choose_editlist.ItemsSource = pa_control.Data_ProjektErtesitendokKapcsolt();
        }


        protected void addColleagueToInterview(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ertesitendok_struct items = btn.DataContext as ertesitendok_struct;
            pa_control.Write_User_To_Inerju(items.id);

            choose_editlist.ItemsSource = pa_control.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = pa_control.Data_InterjuErtesitendokKapcsolt();
        }

        protected void removeColleague(object sender, RoutedEventArgs e)
        {
            if(sess.UserData[0].kategoria == 1)
            {
                MenuItem menu = sender as MenuItem;
                ertesitendok_struct items = menu.DataContext as ertesitendok_struct;
                pa_control.Delete_User_To_Inerju(items.id);
                choose_editlist.ItemsSource = pa_control.Data_ProjektErtesitendokKapcsolt();
                ertesitendok_editlist.ItemsSource = pa_control.Data_InterjuErtesitendokKapcsolt();
            }
        }

        protected void closeColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Hidden;
            Blur_Grid.Visibility = Visibility.Hidden;
        }

        protected void resztvevoSaveClick(object sender, RoutedEventArgs e)
        {
            ertesitendok_editlist.ItemsSource = pa_control.Data_InterjuErtesitendokKapcsolt();
        }

        protected void addColleague(object sender, RoutedEventArgs e)
        {
            EmailTemplate et = new EmailTemplate();
            ControlEmail email = new ControlEmail();
            List<ertesitendok_struct> szemelyek = pa_control.Data_InterjuErtesitendokKapcsolt();
            List<interju_struct> interju = pa_control.Interju_DataSource_ByID();
            List<String> resztvevok = new List<string>();
            foreach (var item in szemelyek)
            {
                resztvevok.Add(item.name);
            }
            foreach (var item in szemelyek)
            {
                email.Mail_Send(item.email, et.Belsos_Meghivo_Email(item.name, interju[0].interju_cim, interju[0].interju_datum+" - " + interju[0].idopont, interju[0].helyszin, interju[0].jelolt_megnevezes));
            }
            email.Mail_Send(interju[0].jelolt_email, et.Jelolt_Meghivo_Email(interju[0].jelolt_megnevezes, interju[0].projekt_megnevezes, interju[0].interju_datum + " - " + interju[0].idopont, resztvevok));
      }
    }
}
