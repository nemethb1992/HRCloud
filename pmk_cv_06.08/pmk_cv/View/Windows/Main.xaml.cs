using HRCloud.Control;
using HRCloud.View.Usercontrol.Panels;
using pmk_cv;
using pmk_cv.Control;
using HRCloud.View.Usercontrol;
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
using System.Windows.Shapes;
using HRCloud.View.Usercontrol.Panels.szakmai_panels;

namespace HRCloud.View.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private applicant_panel applicant_p;
        private project_panel project_p;
        private settings_panel settings_p;
        private Szakmai_Kezdolap szakmai_Kezdolap;
        private favourites_panel favourites_Panel;
        file_cont f_control = new file_cont();
        Session session = new Session();
        //private applicant_DataView applicant_dv;
        //private project_DataView project_dv;
        public Main()
        {
            InitializeComponent();
            if(session.UserData[0].kategoria == 1)
            {
                sgrid.Children.Add(project_p = new project_panel(sgrid));
                HR_navigation_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                sgrid.Children.Add(szakmai_Kezdolap = new Szakmai_Kezdolap(sgrid));
                Szakmai_navigation_Grid.Visibility = Visibility.Visible;
                HR_navigation_Grid.Visibility = Visibility.Hidden;
            }
            //Profil_name_tbl.Text = session.UserData[0].name;
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
            sgrid.Children.Add(project_p = new project_panel(sgrid));
        }
        private void mw_btn2_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_p = new applicant_panel(sgrid));
        }
        private void mw_btn3_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(settings_p = new settings_panel(sgrid));
        }
        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(szakmai_Kezdolap = new Szakmai_Kezdolap(sgrid));
        }

        private void mw_btn4_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(favourites_Panel = new favourites_panel(sgrid));
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

    }
}
