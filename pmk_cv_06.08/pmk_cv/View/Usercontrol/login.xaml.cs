using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HRCloud.Control;
using HRCloud.View.Windows;

namespace HRCloud.View.Usercontrol
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        ControlLogin lcontrol = new ControlLogin();
        Session session = new Session();

        private Grid grid;

        public Login(Grid sgrid)
        {
            this.grid = sgrid;
            InitializeComponent();

            setartUp();
            dbConnectionOpener();
        }

        private void dbConnectionOpener()
        {
            Model.MySql mySql = new Model.MySql();

            if (!mySql.isConnected())
            {
                LoginSign.Text = "Nincs adatkapcsolat!";
            }
        }

        private void loginEnterClick(object sender, RoutedEventArgs e)
        {
            enter();
        }
        
        private void usernameEnterKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enter();
        }

        private void passwordEnterKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enter();
        }
        private void setartUp()
        {
            string user = lcontrol.getRememberedUser();
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
        private void usernameRemember()
        {
            if (login_cbx.IsChecked == true)
            {
                lcontrol.writeRememberedUser(Luser_tbx.Text);
            }
            else
            {
                lcontrol.deleteRememberedUser();
            }
        }
        private void enter()
        {
            //if (lcontrol.ActiveDirectoryValidation(Luser_tbx.Text, Lpass_pwd.Password))
            //{
                if (lcontrol.mySqlUserValidation(Luser_tbx.Text))
            {
                Main mw = new Main();
                    var window = Window.GetWindow(this);
                    session.UserData = lcontrol.Data_UserSession(Luser_tbx.Text);
                    session.tartomanyi = Luser_tbx.Text;

                    usernameRemember();
                    mw.Show();
                    window.Close();
            }

            else
            {
                LoginSign.Text = "Kérem regisztráljon!";
            }
            //}
            //else
            //{
            //    LoginSign.Text = "Sikertelen hitelesítés!";
            //}
        }
        private void navigateToSurveyWindow(object sender, MouseButtonEventArgs e)
        {
            SurveyWindow popup = new SurveyWindow();
            var window = Window.GetWindow(this);

            popup.Show();
            window.Close();
        }       
    }
}
