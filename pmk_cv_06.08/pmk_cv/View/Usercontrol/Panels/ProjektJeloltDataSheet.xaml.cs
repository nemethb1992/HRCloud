using HRCloud.Control;
using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static HRCloud.Control.ControlApplicantProject;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for projekt_jelolt_DataView.xaml
    /// </summary>
    /// 
    public partial class ProjektJeloltDataSheet : UserControl
    {
        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();
        ControlApplicantProject paControl = new ControlApplicantProject();
        Comment comment = new Comment();
        Session session = new Session();

        private ProjectDataSheet projectDataSheet;
        private InterviewPanel interviewPanel;
        private Grid grid;

        public ProjektJeloltDataSheet(Grid grid)
        {
                this.grid = grid;
                InitializeComponent();
                projectFormLoader();
        }

        protected void projectFormLoader()
        {
            List<JeloltExtendedList> a_li = aControl.Data_JeloltFull();
            List<ProjectExtendedListItems> p_li = pControl.Data_ProjectFull();
            List<kompetenciak> li_k = paControl.Data_Kompetencia();
            List<kompetencia_summary_struct> li_kvalue = paControl.Data_KompetenciaJeloltKapcs();

            projekt_jelolt_title_tbl.Text = p_li[0].megnevezes_projekt + " - " + a_li[0].nev;
            jelolt_telefon.Text = "( " + a_li[0].telefon + " )";
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentKapcs();
            kapcs_jeloltek_listBox.ItemsSource = paControl.Data_Interview();
            inter_cim.Items.Add("HR interjú");
            inter_cim.Items.Add("Szakmai + HR");
            inter_cim.Items.Add("Szakmai Interjú 1.");
            inter_cim.Items.Add("Szakmai Interjú 2.");
            inter_cim.Items.Add("Szakmai Interjú 3.");
            inter_cim.Items.Add("Szakmai Interjú 4.");
            inter_cim.SelectedIndex = 0;
            jeloltTamogatasa();
            try
            {
                k_1.Value = li_kvalue[0].k1_val;
                k_2.Value = li_kvalue[0].k2_val;
                k_3.Value = li_kvalue[0].k3_val;
                k_4.Value = li_kvalue[0].k4_val;
                k_5.Value = li_kvalue[0].k5_val;
            }
            catch (Exception)
            {
            }
            foreach (var item in li_k)
            {
                if (item.id == p_li[0].kepesseg1)
                { kompetencia_1.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg2)
                { kompetencia_2.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg3)
                { kompetencia_3.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg4)
                { kompetencia_4.Text = item.kompetencia_megnevezes; }
                if (item.id == p_li[0].kepesseg5)
                { kompetencia_5.Text = item.kompetencia_megnevezes; }
            }
            telephoneInspectedLayout();
        }

        protected void telephoneInspectedLayout()
        {
            if (paControl.TelefonSzurt == 1)
            {
                telefonos_igen_btn.Visibility = Visibility.Hidden;
                szurt_tbl.Visibility = Visibility.Visible;
            }
        }

        protected void backToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
        }

        protected void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.add(comment_tartalom.Text, pControl.ProjektID, aControl.ApplicantID, 0);
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentKapcs();
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

        protected void commentDeleteMenuItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;

            comment.delete(items.id, session.UserData[0].id, pControl.ProjektID, aControl.ApplicantID);
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentKapcs();
        }

        protected void telephonePanelOpenClick(object sender, RoutedEventArgs e)
        {
            ismerte_cbx.Items.Add("nem");
            ismerte_cbx.Items.Add("igen");
            ismerte_cbx.SelectedIndex = 0;
            grid_telefonosszuro.Height = 400;
            telefonos_igen_btn.IsEnabled = false;
            telefonos_nem_btn.IsEnabled = false;
        }

        protected void telephonePanelCloseClick(object sender, RoutedEventArgs e)
        {
            grid_telefonosszuro.Height = 100;
            ismerte_cbx.Items.Clear();
            muszakok_tbx.Text = "";
            utazas_tbx.Text = "";
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
        }

        protected void telephoneAcceptClick(object sender, RoutedEventArgs e)
        {
            int ismerte = 0;

            if(ismerte_cbx.SelectedItem == "igen")
            {
                ismerte = 1;
            }
            paControl.telephoneFilterInsert(ismerte,Convert.ToInt32(muszakok_tbx.Text),utazas_tbx.Text);
            paControl.TelefonSzurt = 1;
            grid_telefonosszuro.Height = 100;
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
            telephoneInspectedLayout();
        }

        protected void telephoneDeclineClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan elutasítja? \n", "HR Cloud", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    pControl.jeloltKapcsUpdate(aControl.ApplicantID, 3);
                    grid.Children.Clear();
                    grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void interviewPanelOpen()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");

            grid.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
        }

        protected void interviewPanelClose()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");

            grid.Visibility = Visibility.Hidden;
            grid2.Visibility = Visibility.Hidden;
        }

        protected void Uj_Interju_felvetele_Click(object sender, RoutedEventArgs e)
        {
            interviewPanelOpen();
        }

        protected void uj_interju_megsem_btn_Click(object sender, RoutedEventArgs e)
        {
            interviewPanelClose();
        }

        protected void ui_bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            interviewPanelClose();
        }

        protected void uj_interju_mentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string datum = "";
            string[] seged = inter_date.SelectedDate.ToString().Split(' ');
            try
            {
                datum = seged[0] + seged[1] + seged[2];
            }
            catch{ }
            try
            {
                paControl.addInterview(datum, inter_cim.SelectedItem.ToString(), inter_leiras.Text, inter_helyszin.Text, inter_idopont.Text);
                projectFormLoader();
                interviewPanelClose();
            }
            catch (Exception)
            {
                MessageBox.Show("Nincs kitöltve minden mező!");
            }
        }

        protected void navigateToInterviewPanel(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            interju_struct items = btn.DataContext as interju_struct;

            paControl.InterjuID = items.id;
            grid.Children.Clear();
            grid.Children.Add(interviewPanel = new InterviewPanel(grid));
        }

        protected void deleteInterview(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            interju_struct items = menu.DataContext as interju_struct;

            paControl.interviewDelete(items.id);
            projectFormLoader();
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void jeloltTamogatasa()
        {
            List<kompetencia_tamogatas> list = new List<kompetencia_tamogatas>();

            list = paControl.Data_KompetenciaTamogatas();
            int igen = 0, ossz = 0 ;
            foreach (var item in list)
            {
                ossz++;
                if (item.tamogatom == 1)
                {
                    igen++;
                }

            }
            tamogatasok_input_tbx.Text = igen.ToString() + "/" + ossz.ToString();
        }
    }

}
