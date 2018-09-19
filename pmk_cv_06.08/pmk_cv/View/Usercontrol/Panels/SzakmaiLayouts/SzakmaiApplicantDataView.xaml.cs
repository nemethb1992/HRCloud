using HRCloud.Control;
using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HRCloud.Source;

namespace HRCloud.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for Szakmai_applicant_DataView.xaml
    /// </summary>
    public partial class SzakmaiApplicantDataView : UserControl
    {
        Comment comment = new Comment();
        Session session = new Session();
        ControlApplicant aControl = new ControlApplicant();

        private SzakmaiProjektDataSheet szakmaiProjektDataSheet;
        private Grid grid;

        public SzakmaiApplicantDataView(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            formLoader();
        }

        protected void formLoader()
        {
            List<JeloltExtendedList> list = aControl.Data_JeloltFull();
            applicant_profile_title.Text = list[0].nev;
            app_input_1.Text = list[0].email;
            app_input_2.Text = list[0].telefon.ToString();
            app_input_3.Text = list[0].lakhely;
            app_input_5.Text = list[0].nyelvtudas.ToString();
            app_input_8.Text = list[0].munkakor;
            app_input_9.Text = list[0].ertesult.ToString();
            app_input_10.Text = list[0].szuldatum.ToString();
            commentLoader(megjegyzes_listBox);
        }

        protected void commentLoader(ListBox lb)
        {
            lb.ItemsSource = aControl.Data_Comment();
        }

        protected void commentDelete(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            megjegyzes_struct items = item.DataContext as megjegyzes_struct;

            comment.delete(items.id, session.UserData[0].id, 0, aControl.ApplicantID);
            commentLoader(megjegyzes_listBox);
        }

        protected void addComment(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.add(comment_tartalom.Text, 0, aControl.ApplicantID, 0);
            commentLoader(megjegyzes_listBox);
            textbox.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "Új megjegyzés")
            {
                textbox.Text = "";
            }
        }

        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "")
            {
                textbox.Text = "Új megjegyzés";
            }
        }

        protected void navigateToSzakmaiProjektDataSheet(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiProjektDataSheet = new SzakmaiProjektDataSheet(grid));
        }
    }
}
