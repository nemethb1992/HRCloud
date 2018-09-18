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
    /// Interaction logic for SzakmaiProjekt_DataView.xaml
    /// </summary>
    public partial class SzakmaiProjektDataSheet : UserControl
    {

        ControlProject pControl = new ControlProject();
        ControlApplicant aControl = new ControlApplicant();
        ControlApplicantProject paControl = new ControlApplicantProject();
        Session session = new Session();
        Comment comment = new Comment();

        private SzakmaiApplicantDataView szakmaiApplicantDataView;
        private SzakmaiList szakmaiList;
        private Grid grid;

        public SzakmaiProjektDataSheet(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            formLoader();
        }

        protected void navigateToSzakmaiList(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiList = new SzakmaiList(grid));
        }

        protected void formLoader()
        {
            List<ProjectExtendedListItems> list = pControl.Data_ProjectFull();
            projekt_profile_title.Text = list[0].megnevezes_projekt;
            projekt_input_1.Text = list[0].statusz.ToString();
            projekt_input_2.Text = list[0].megnevezes_munka;
            projekt_input_3.Text = list[0].megnevezes_pc;
            projekt_input_4.Text = list[0].megnevezes_vegzettseg;
            projekt_input_5.Text = list[0].megnevezes_nyelv;
            projekt_input_6.Text = list[0].jeloltek_db.ToString();
            projekt_input_7.Text = list[0].megnevezes_hr;
            projekt_input_8.Text = list[0].fel_datum.ToString();
            projekt_input_9.Text = list[0].ber.ToString() + " Ft";
            projekt_input_10.Text = list[0].tapasztalat_ev.ToString();

            List<kompetenciak> listKompetencia = paControl.Data_Kompetencia();
            foreach (var item in listKompetencia)
            {
                if (item.id == list[0].kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            megjegyzes_listBox.ItemsSource = pControl.Data_CommentProject();
            kapcs_jeloltek_listBox.ItemsSource = pControl.Data_JeloltKapcs();
        }

        protected void commentDelete(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            megjegyzes_struct items = menuItem.DataContext as megjegyzes_struct;

            comment.delete(items.id, session.UserData[0].id, pControl.ProjektID, 0);
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentProject();
        }

        protected void addComment(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.add(comment_tartalom.Text, pControl.ProjektID, 0, 0);
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentProject();
            tbx.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "Új megjegyzés")
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

        protected void openApplicant(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;

            aControl.ApplicantID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmaiApplicantDataView = new SzakmaiApplicantDataView(grid));
        }
    }
}
