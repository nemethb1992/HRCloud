using HRCloud.Control;
using HRCloud.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static HRCloud.Model.ModelEmail;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Applicant_DataView.xaml
    /// </summary>
    public partial class ApplicantDataSheet : UserControl
    {
        Comment comment = new Comment();
        ControlApplicant acontrol = new ControlApplicant();
        ControlProject pcontrol = new ControlProject();
        ControlFile f_control = new ControlFile();
        Session sess = new Session();

        private ProjectDataSheet projectDataSheet;
        private Grid grid;

        public ApplicantDataSheet(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            formLoader();
        }
        protected void formLoader()
        {
            List<JeloltExtendedList> li = acontrol.Data_JeloltFull();
            applicant_profile_title.Text = li[0].nev;
            app_input_1.Text = li[0].email;
            app_input_2.Text = li[0].telefon.ToString();
            app_input_3.Text = li[0].lakhely;
            app_input_5.Text = li[0].nyelvtudas.ToString();
            app_input_6.Text = li[0].nyelvtudas2.ToString();
            app_input_8.Text = li[0].munkakor;
            app_input_9.Text = li[0].ertesult.ToString();
            app_input_10.Text = li[0].szuldatum.ToString();
            projekt_cbx.ItemsSource = acontrol.Data_PorjectListSmall();
            csatolmany_listBox.ItemsSource = f_control.Applicant_FolderReadOut(acontrol.ApplicantID);
            commentLoader(megjegyzes_listBox);
            kapcsolodo_projekt_list.ItemsSource = acontrol.Data_ProjectList();
        }


        protected void commentLoader(ListBox lb)
        {
            lb.ItemsSource = acontrol.Data_Comment();
        }

        protected void navigateToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SmallProjectListItems items = button.DataContext as SmallProjectListItems;
            pcontrol.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
        }

        protected void projectDelete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            SmallProjectListItems items = delete.DataContext as SmallProjectListItems;
            acontrol.applicalntProjectListDelete(items.id);
            kapcsolodo_projekt_list.ItemsSource = acontrol.Data_ProjectList();
        }

        protected void commentDelete(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            megjegyzes_struct items = item.DataContext as megjegyzes_struct;
            comment.delete(items.id, sess.UserData[0].id, 0, acontrol.ApplicantID);
            commentLoader(megjegyzes_listBox);
        }

        protected void textBoxKeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.megjegyzes_feltoltes(comment_tartalom.Text, 0,acontrol.ApplicantID, 0);
            commentLoader(megjegyzes_listBox);
            textbox.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if(tbx.Text == "Új megjegyzés")
            {
                tbx.Text = "";
            }
        }
        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Text == "")
            {
                tbx.Text = "Új megjegyzés";
            }
        }

        protected void projektClick(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = projekt_cbx as ComboBox;
            SmallProjectListItems item = cbx.SelectedItem as SmallProjectListItems;
            pcontrol.Jelolt_write_to_project(acontrol.ApplicantID , item.id);
            kapcsolodo_projekt_list.ItemsSource = acontrol.Data_ProjectList();
        }

        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Jelolt_File_Struct item = btn.DataContext as Jelolt_File_Struct;
            Process.Start(item.path);
        }
    }
}
