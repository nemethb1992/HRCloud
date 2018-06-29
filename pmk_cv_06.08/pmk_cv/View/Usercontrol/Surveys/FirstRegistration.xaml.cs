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
using HRCloud.View.Usercontrol;

namespace HRCloud.View.Usercontrol.Surveys
{
    /// <summary>
    /// Interaction logic for FirstRegistration.xaml
    /// </summary>
    public partial class FirstRegistration : UserControl
    {
        private Grid grid;
        login_cont l_control = new login_cont();
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
            if (teljesnev.Text != "" && email.Text != "")
            {
                ComboBox katcbx = kategoria_cbx as ComboBox;
                Kategoria_struct kategoria_items = katcbx.SelectedItem as Kategoria_struct;
                l_control.UserRegistration(tartomanyi.Text, teljesnev.Text, email.Text, kategoria_items.id);
                MainWindow login = new MainWindow();
                var window = Window.GetWindow(this);
                window.Close();
                login.Show();
            }
            else
            {
                InfoBlock.Text = "Kitöltetlen mező!";
            }
        }
    }
}
