using HRCloud.Control;
using HRCloud.Model;
using Microsoft.Win32;
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
    /// Interaction logic for Applicant_DataView.xaml
    /// </summary>
    public partial class applicant_DataView : UserControl
    {
        private Grid grid;
        activity_log acti = new activity_log();
        megjegyzes_cs comment = new megjegyzes_cs();
        applicant_cont acontrol = new applicant_cont();
        projekt_cont pcontrol = new projekt_cont();
        Session sess = new Session();
        private project_DataView project_DataView;
        public applicant_DataView(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            FormLoader();
        }
        void FormLoader()
        {
            List<JeloltExtendedList> li = acontrol.JeloltFullDataSource();
            applicant_profile_title.Text = li[0].nev;
            app_input_1.Text = li[0].email;
            app_input_2.Text = li[0].telefon.ToString();
            app_input_3.Text = li[0].lakhely;
            //app_input_4.Text = li[0].cim;
            app_input_5.Text = li[0].nyelvtudas.ToString();
            app_input_6.Text = li[0].nyelvtudas2.ToString();
            //app_input_7.Text = li[0].berigeny.ToString();
            app_input_8.Text = li[0].munkakor;
            app_input_9.Text = li[0].ertesult.ToString(); 
            app_input_10.Text = li[0].szuldatum.ToString();
            csatolmany_listBox.ItemsSource = acontrol.CsatolmanyDataSource();
            megjegyzes_listBox_loadUp(megjegyzes_listBox);
            kapcsolodo_projekt_list.ItemsSource = acontrol.ProjektListSourceForListBox();
        }


        private void csatolmany_download_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            csatolmany_struct items = btn.DataContext as csatolmany_struct;
            if (!acontrol.CvDownload(items.kapcs_id.ToString(), items.fajlnev, items.kiterjesztes))
                MessageBox.Show("Letöltés megszakadt!");
        }

        private void projekt_upload_btn_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
            }
            MessageBox.Show(path);
            if (!acontrol.CvUpload(path))
                MessageBox.Show("Feltöltés megszakadt!");
            acti.Activity_write();
            csatolmany_listBox.ItemsSource = acontrol.CsatolmanyDataSource();

        }
        private void megjegyzes_listBox_loadUp(ListBox lb)
        {
            lb.ItemsSource = acontrol.megjegyzes_datasource();
        }

        private void tovabb_projektre_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SmallProjectListItems items = button.DataContext as SmallProjectListItems;
            pcontrol.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(project_DataView = new project_DataView(grid));
        }

        private void Projekt_Link_Delete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            SmallProjectListItems items = delete.DataContext as SmallProjectListItems;
            acontrol.projekt_list_delete(items.id);
            kapcsolodo_projekt_list.ItemsSource = acontrol.ProjektListSourceForListBox();
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
            comment.megjegyzes_feltoltes(comment_tartalom.Text, 0,acontrol.ApplicantID, 0);
            megjegyzes_listBox_loadUp(megjegyzes_listBox);
            tbx.Text = "";
        }

        private void comment_tartalom_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if(tbx.Text == "Új megjegyzés")
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
        
        private void csatolmany_listBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
                int i = 0;
                foreach (var item in path)
                {
                    acontrol.CvUpload(path[i]);
                    i++;
                }
                MessageBox.Show(path[0]);
                csatolmany_listBox.ItemsSource = acontrol.CsatolmanyDataSource();
                // docPath[0] tartalmazza az URL-t

            }
        }
    }
}
