using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HRCloud.Control;
using HRCloud.Source;

namespace HRCloud.View.Usercontrol.Surveys
{
    /// <summary>
    /// Interaction logic for FirstRegistration.xaml
    /// </summary>
    public partial class FirstRegistration : UserControl
    {
        private Grid grid;
        ControlLogin l_control = new ControlLogin();
        Session session = new Session();
        public FirstRegistration(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            StartMethods();
        }

        public class Kategoria_struct
        {
            public int id { get; set; }
            public string nev { get; set; }
        }
        void StartMethods()
        {
            List<Kategoria_struct> list = new List<Kategoria_struct>();
            list.Add(new Kategoria_struct { id = 0, nev = "Szakmai felelős"});
            list.Add(new Kategoria_struct { id = 1, nev = "HR felelős" });
            kategoria_cbx.ItemsSource = list;
            kategoria_cbx.SelectedIndex = 0;
        }
        private void Back_Click_btn(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            var window = Window.GetWindow(this);
            window.Close();
            login.Show();
        }
        private void Registration_Click_btn(object sender, RoutedEventArgs e)
        {

            if (teljesnev.Text.Length > 0 && email.Text.Length > 0 && tartomanyi.Text.Length > 0 && tartomanyi_pass.Password.Length > 0)
            {
                if(l_control.ActiveDirectoryValidation(tartomanyi.Text,tartomanyi_pass.Password))
                {
                    ComboBox katcbx = kategoria_cbx as ComboBox;
                    Kategoria_struct kategoria_items = katcbx.SelectedItem as Kategoria_struct;
                    l_control.userRegistration(tartomanyi.Text, teljesnev.Text, email.Text, kategoria_items.id);
                    MainWindow login = new MainWindow();
                    var window = Window.GetWindow(this);
                    window.Close();
                    login.Show();
                }
                else
                {
                    InfoBlock.Text = "Hibás hitelesítés!";
                }
            }
            else
            {
                InfoBlock.Text = "Kitöltetlen mező!";
            }
        }

        private void Registration_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tartomanyi.Text.Length > 0 && tartomanyi_pass.Password.Length > 0 && teljesnev.Text.Length > 0 && email.Text.Length > 0)
            {
                Registration_btn.IsEnabled = true;
            }
            else
            {
                Registration_btn.IsEnabled = false;
            }
        }

        private void tartomanyi_pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tartomanyi.Text.Length > 0 && tartomanyi_pass.Password.Length > 0 && teljesnev.Text.Length > 0 && email.Text.Length > 0)
            {
                Registration_btn.IsEnabled = true;
            }
            else
            {
                Registration_btn.IsEnabled = false;
            }
        }
    }
}
