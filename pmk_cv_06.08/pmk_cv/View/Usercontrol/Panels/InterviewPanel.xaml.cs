using HRCloud.Control;
using HRCloud.Model;
using HRCloud.Public.templates;
using HRCloud.View.Usercontrol.Panels.SzakmaiLayouts;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HRCloud.Source;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for interju_panel.xaml
    /// </summary>
    public partial class InterviewPanel : UserControl
    {
        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();
        ControlApplicantProject paControl = new ControlApplicantProject();
        Session session = new Session();

        private Grid grid;
        private ProjektJeloltDataSheet projektJeloltDataSheet;
        private SzakmaiInterviewList szakmaiInterviewList;

        public InterviewPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();

            interviewLoader();
            if (session.UserData[0].kategoria == 0)
            {
                addPerson.Visibility = Visibility.Hidden;
                invitePerson.Visibility = Visibility.Hidden;
            }
        }

        protected void navigateBackFromInterview(object sender, RoutedEventArgs e)
        {
            if(session.UserData[0].kategoria == 1)
            {
                grid.Children.Clear();
                grid.Children.Add(projektJeloltDataSheet = new ProjektJeloltDataSheet(grid));
            }
            else
            {
                grid.Children.Clear();
                grid.Children.Add(szakmaiInterviewList = new SzakmaiInterviewList(grid));
            }
        }

        protected void interviewLoader()
        {
            List<interju_struct> list = paControl.Data_InterviewById();
            List<ProjectExtendedListItems> li = pControl.Data_ProjectFull();
            List<kompetenciak> li_k = paControl.Data_Kompetencia();

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

            choose_editlist.ItemsSource = paControl.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = paControl.Data_InterjuErtesitendokKapcsolt();

            if (paControl.hasKompetencia())
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
            List<ProjectExtendedListItems> li = pControl.Data_ProjectFull();
            List<int> list = new List<int>();

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

            paControl.kompetenciaUpdate(list);
            Panel.SetZIndex(kompetencia_border,1);
            locked_title.Visibility = Visibility.Visible;
            teszt_nyitas_btn.Visibility = Visibility.Visible;


        }

        protected void openColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Visible;
            Blur_Grid.Visibility = Visibility.Visible;
            choose_editlist.ItemsSource = paControl.Data_ProjektErtesitendokKapcsolt();
        }
        
        protected void addColleagueToInterview(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ertesitendok_struct items = btn.DataContext as ertesitendok_struct;

            paControl.insertInterviewInvited(items.id);
            choose_editlist.ItemsSource = paControl.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = paControl.Data_InterjuErtesitendokKapcsolt();
        }

        protected void removeColleague(object sender, RoutedEventArgs e)
        {
            if(session.UserData[0].kategoria == 1)
            {
                MenuItem menu = sender as MenuItem;
                ertesitendok_struct items = menu.DataContext as ertesitendok_struct;

                paControl.deleteInterviewInvited(items.id);
                choose_editlist.ItemsSource = paControl.Data_ProjektErtesitendokKapcsolt();
                ertesitendok_editlist.ItemsSource = paControl.Data_InterjuErtesitendokKapcsolt();
            }
        }

        protected void closeColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Hidden;
            Blur_Grid.Visibility = Visibility.Hidden;
        }

        protected void resztvevoSaveClick(object sender, RoutedEventArgs e)
        {
            ertesitendok_editlist.ItemsSource = paControl.Data_InterjuErtesitendokKapcsolt();
        }

        protected void addColleague(object sender, RoutedEventArgs e)
        {
            EmailTemplate et = new EmailTemplate();
            ControlEmail email = new ControlEmail();
            List<ertesitendok_struct> szemelyek = paControl.Data_InterjuErtesitendokKapcsolt();
            List<interju_struct> interju = paControl.Data_InterviewById();
            List<String> resztvevok = new List<string>();

            foreach (var item in szemelyek)
            {
                resztvevok.Add(item.name);
            }
            foreach (var item in szemelyek)
            {
                email.sendMail(item.email, et.Belsos_Meghivo_Email(item.name, interju[0].interju_cim, interju[0].interju_datum+" - " + interju[0].idopont, interju[0].helyszin, interju[0].jelolt_megnevezes));
            }
            email.sendMail(interju[0].jelolt_email, et.Jelolt_Meghivo_Email(interju[0].jelolt_megnevezes, interju[0].projekt_megnevezes, interju[0].interju_datum + " - " + interju[0].idopont, resztvevok));
      }
    }
}
