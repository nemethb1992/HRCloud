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
using HRCloud.Control;
using HRCloud.Model;
using HRCloud.Public.templates;
using HRCloud.View;
using HRCloud.View.Windows;
using pmk_cv.Control;

namespace HRCloud.View.Usercontrol
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        ControlLogin lcontrol = new ControlLogin();
        Session session = new Session();
        Model.MySql dbE = new Model.MySql();
        ControlEmail e_control = new ControlEmail();
        private Grid grid;
        public Login(Grid sgrid)
        {
            this.grid = sgrid;
            InitializeComponent();
            BootMethods();
            dbConnectionOpener();

        }
        void dbConnectionOpener()
        {
            if (!dbE.dbOpen())
            {
                LoginSign.Text = "Nincs adatkapcsolat!";
            }
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            enterApplication();
        }
        
        private void Luser_tbx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enterApplication();
        }

        private void Lpass_pwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enterApplication();
        }
        private void BootMethods()
        {
            
            string user = lcontrol.GetRememberedUser();
            if (user != "")
            {
                Luser_tbx.Text = user;
                login_cbx.IsChecked = true;
            }
            else
            {
                login_cbx.IsChecked = false;
            }
        }
        private void UserRemember()
        {
            if (login_cbx.IsChecked == true)
            {
                lcontrol.WriteRememberedUser(Luser_tbx.Text);
            }
            else
            {
                lcontrol.DeleteRememberedUser();
            }
        }
        private void enterApplication()
        {


            if (lcontrol.ActiveDirectoryValidation(Luser_tbx.Text, Lpass_pwd.Password))
            {
                if (lcontrol.UserValider_MySql(Luser_tbx.Text))
                {
                    UserRemember();
                    session.UserData = lcontrol.UserSessionDataList(Luser_tbx.Text);
                    session.tartomanyi = Luser_tbx.Text;
                    Main mw = new Main();
                    mw.Show();
                    var window = Window.GetWindow(this);
                    window.Close();
                }

            else
            {
                LoginSign.Text = "Kérem regisztráljon!";
            }
        }
            else
            {
                LoginSign.Text = "Sikertelen hitelesítés!";
            }
}

        private void Registration_Click(object sender, MouseButtonEventArgs e)
        {
            SurveyWindow popup = new SurveyWindow();
            popup.Show();
            var window = Window.GetWindow(this);
            window.Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    List<string> li = new List<string>();
        //    li.Add("Drozdik Szilvia");
        //    li.Add("Béda Olga");
        //    li.Add("Szakmai Felelős1");
        //    li.Add("Szakmai Felelős2");
        //    email_template temp = new email_template();
        //    e_control.Mail_Send("fzbalu92@gmail.com", temp.Jelolt_Meghivo_Email("Németh Balázs", "Gyártósori összeszerelő","2018.08.20. - 14:30",li ));
        //    //e_control.Mail_Send("info@betapress.hu", etemp.Udvozlo_Email("Név"));
        //    //e_control.Mail_Send("fzbalu92@gmail.com", etemp.Elutasito_Email("Név"));
        //}
        
    }
}
