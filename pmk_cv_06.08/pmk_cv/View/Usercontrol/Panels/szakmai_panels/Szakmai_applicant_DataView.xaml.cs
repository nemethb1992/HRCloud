using HRCloud.Control;
using HRCloud.Model;
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

namespace HRCloud.View.Usercontrol.Panels.szakmai_panels
{
    /// <summary>
    /// Interaction logic for Szakmai_applicant_DataView.xaml
    /// </summary>
    public partial class Szakmai_applicant_DataView : UserControl
    {
        private Grid grid;
        megjegyzes_cs comment = new megjegyzes_cs();
        applicant_cont acontrol = new applicant_cont();
        private SzakmaiProjekt_DataView szDataView;
        Session sess = new Session();
        public Szakmai_applicant_DataView(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            FormLoader();
        }
        void FormLoader()
        {
            List<JeloltExtendedList> li = acontrol.JeloltFullDataSource();
            applicant_profile_title.Text = li[0].nev;
            app_input_1.Text = li[0].email;
            app_input_2.Text = li[0].telefon.ToString();
            app_input_3.Text = li[0].lakhely;
            app_input_5.Text = li[0].nyelvtudas.ToString();
            //app_input_6.Text = li[0].nyelvtudas_szint.ToString();
            //app_input_7.Text = li[0].berigeny.ToString();
            app_input_8.Text = li[0].munkakor;
            app_input_9.Text = li[0].ertesult.ToString();
            app_input_10.Text = li[0].szuldatum.ToString();
            csatolmany_listBox.ItemsSource = acontrol.CsatolmanyDataSource();
            megjegyzes_listBox_loadUp(megjegyzes_listBox);
        }
        private void megjegyzes_listBox_loadUp(ListBox lb)
        {
            lb.ItemsSource = acontrol.megjegyzes_datasource();
        }
        private void Megjegyzes_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;
            comment.megjegyzes_torles(items.id, sess.UserData[0].id, 0, acontrol.ApplicantID);
            megjegyzes_listBox_loadUp(megjegyzes_listBox);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.megjegyzes_feltoltes(comment_tartalom.Text, 0, acontrol.ApplicantID, 0);
            megjegyzes_listBox_loadUp(megjegyzes_listBox);
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
        private void szakmai_applicant_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szDataView = new SzakmaiProjekt_DataView(grid));
        }
    }
}
