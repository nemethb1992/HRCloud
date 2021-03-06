﻿using HRCloud.Control;
using HRCloud.View.Usercontrol.Panels;
using System.Windows;
using System.Windows.Input;
using HRCloud.View.Usercontrol.Panels.SzakmaiLayouts;
using HRCloud.Source;

namespace HRCloud.View.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private ApplicantList applicant_p;
        private ProjectList project_p;
        private SettingsPanel settings_p;
        private SzakmaiKezdolap szakmai_Kezdolap;
        private FavouritesPanel favourites_Panel;
        ControlFile f_control = new ControlFile();
        Session session = new Session();
        //private applicant_DataView applicant_dv;
        //private project_DataView project_dv;
        public Main()
        {
            InitializeComponent();
            if (session.UserData[0].kategoria == 1)
            {
                sgrid.Children.Add(project_p = new ProjectList(sgrid));
                HR_navigation_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                sgrid.Children.Add(szakmai_Kezdolap = new SzakmaiKezdolap(sgrid));
                Szakmai_navigation_Grid.Visibility = Visibility.Visible;
                HR_navigation_Grid.Visibility = Visibility.Hidden;
            }
            //f_control.Applicant_Folder_Structure_Creator();
            //f_control.Projekt_Folder_Structure_Creator();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            var window = Window.GetWindow(this);
            window.Close();
            mw.Show();
        }
        private void mw_btn1_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(project_p = new ProjectList(sgrid));
        }
        private void mw_btn2_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_p = new ApplicantList(sgrid));
        }
        private void mw_btn3_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(settings_p = new SettingsPanel(sgrid));
        }
        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(szakmai_Kezdolap = new SzakmaiKezdolap(sgrid));
        }

        private void mw_btn4_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(favourites_Panel = new FavouritesPanel(sgrid));
        }

        //private void profil_notiBtn_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    profil_notiRec.Fill = new SolidColorBrush(Colors.White);
        //}
        //private void profil_settBtn_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    profil_settRec.Fill = new SolidColorBrush(Colors.White);
        //}
        //private void profil_settBtn_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    profil_settRec.Fill = new SolidColorBrush(Colors.Transparent);
        //}
        //private void profil_notiBtn_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    profil_notiRec.Fill = new SolidColorBrush(Colors.Transparent);
        //}
        private void Maximize_Window_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    login_cont lcont = new login_cont();
        //    string user = testtbx.Text;
        //    string pass = testtbx2.Text;
        //    bool valid = lcont.ActiveDirectoryValidation(user,pass);
        //    if (valid == true)
        //    {
        //        info.Text = "Siker";
        //    }
        //    else
        //    {
        //        info.Text = "Nem";
        //    }
        //}
    }
}
