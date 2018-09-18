using HRCloud.Control;
using HRCloud.Model;
using HRCloud.Public.templates;
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

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_DataView.xaml
    /// </summary>
    public partial class ProjectDataSheet : UserControl
    {
        ControlApplicantProject paControl = new ControlApplicantProject();
        ControlProject pControl = new ControlProject();
        ControlApplicant aControl = new ControlApplicant();
        EmailTemplate emailTemplate = new EmailTemplate();
        ControlEmail email = new ControlEmail();
        Comment comment = new Comment();
        Session session = new Session();

        private Grid grid;
        private ProjektJeloltDataSheet projektJeloltDataSheet;
        private ProjectList projectList;

        public ProjectDataSheet(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            try
            {
                formLoader();
            }
            catch (Exception)
            {
                grid.Children.Clear();
                grid.Children.Add(projectList = new ProjectList(grid));
            }
        }

        protected void formLoader()
        {
            List<ProjectExtendedListItems> li = pControl.Data_ProjectFull();
            projekt_profile_title.Text = li[0].megnevezes_projekt;
            projekt_input_1.Text = li[0].statusz.ToString();
            projekt_input_2.Text = li[0].megnevezes_munka;
            projekt_input_3.Text = li[0].megnevezes_pc;
            projekt_input_4.Text = li[0].megnevezes_vegzettseg;
            projekt_input_5.Text = li[0].megnevezes_nyelv;
            projekt_input_6.Text = li[0].jeloltek_db.ToString();
            projekt_input_7.Text = li[0].megnevezes_hr;
            projekt_input_8.Text = li[0].fel_datum.ToString();
            projekt_input_9.Text = li[0].ber.ToString() + " Ft";
            projekt_input_10.Text = li[0].tapasztalat_ev.ToString();

            List<kompetenciak> li_k = paControl.Data_Kompetencia();
            foreach (var item in li_k)
            {
                if(item.id == li[0].kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            feladatok_tbx.Text = li[0].feladatok;
            elvarasok_tbx.Text = li[0].elvarasok;
            kinalunk_tbx.Text = li[0].kinalunk;
            elonyok_tbx.Text = li[0].elonyok;
            projectCost();

            if(li[0].publikalt == 1)
            {
                publikalt_check.IsChecked = true;
            }
            listLoader();
        }

        protected void listLoader()
        {
            megjegyzes_listBox.ItemsSource = pControl.Data_CommentProject();
            kapcs_jeloltek_listBox.ItemsSource = pControl.Data_JeloltKapcs();
            kapcs_ertesitendo_listBox.ItemsSource = pControl.Data_ErtesitendokKapcs();
            kapcs_hr_listBox.ItemsSource = pControl.Data_HrProject();
            koltseg_listBox.ItemsSource = pControl.Data_ProjectCost();
        }

        protected void openApplicantClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;

            aControl.ApplicantID = items.id;

            if(items.allapota >= 1)
            {
                paControl.TelefonSzurt = 1;
            }
            else
            {
                paControl.TelefonSzurt = 0;
            }
            grid.Children.Clear();
            grid.Children.Add(projektJeloltDataSheet = new ProjektJeloltDataSheet(grid));
        }

        protected void jeloltTabClick(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Visible;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Hidden;

            jeloltek_addbtn.Visibility = System.Windows.Visibility.Visible;
            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Hidden;
            hr_addbtn.Visibility = System.Windows.Visibility.Hidden;
        }

        protected void ertesitendokTabClick(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Visible;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Hidden;

            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Visible;
            jeloltek_addbtn.Visibility = System.Windows.Visibility.Hidden;
            hr_addbtn.Visibility = System.Windows.Visibility.Hidden;
        }

        protected void hrTabClick(object sender, RoutedEventArgs e)
        {
            kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Hidden;
            kapcs_hr_listBox.Visibility = System.Windows.Visibility.Visible;

            hr_addbtn.Visibility = System.Windows.Visibility.Visible;
            ertesitendok_addbtn.Visibility = System.Windows.Visibility.Hidden;
            jeloltek_addbtn.Visibility = System.Windows.Visibility.Hidden;
            
        }

        //private void add_applicant_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = applicant_cbx as ComboBox;
        //    JeloltListItems jelolt = cbx.SelectedItem as JeloltListItems;
        //    pcontrol.Jelolt_write_to_project(jelolt.id);
        //    FormLoader();
        //}

        //private void add_hr_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = hr_cbx as ComboBox;
        //    hr_struct items = cbx.SelectedItem as hr_struct;
        //    pcontrol.HR_write_to_project(items.id);
        //    KisListaTolto(megjegyzes_listBox);
        //}

        //private void add_felelos_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    ComboBox cbx = felelos_cbx as ComboBox;
        //    ertesitendok_struct items = cbx.SelectedItem as ertesitendok_struct;
        //    pcontrol.Ertesitendok_write_to_project(items.id);
        //    KisListaTolto(megjegyzes_listBox);
        //}

        protected void jeloltDeleteClick(object sender, MouseButtonEventArgs e)
        {
            Image delete = sender as Image;
            JeloltListItems items = delete.DataContext as JeloltListItems;

            pControl.jeloltKapcsDelete(items.id);
            kapcs_jeloltek_listBox.ItemsSource = pControl.Data_JeloltKapcs();
        }

        protected void ertesitendoDeleteClick(object sender, RoutedEventArgs e)
        {
            Button delete = sender as Button;
            ertesitendok_struct items = delete.DataContext as ertesitendok_struct;

            pControl.ertesitendokKapcsDelete(items.id);
            kapcs_ertesitendo_listBox.ItemsSource = pControl.Data_ErtesitendokKapcs();
        }

        protected void hrDeleteClick(object sender, RoutedEventArgs e)
        {
            Button delete = sender as Button;
            hr_struct items = delete.DataContext as hr_struct;

            pControl.hrKapcsDelete(items.id);
            kapcs_hr_listBox.ItemsSource = pControl.Data_HrProject();
        }


        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            megjegyzes_struct items = delete.DataContext as megjegyzes_struct;

            comment.delete(items.id, session.UserData[0].id, pControl.ProjektID, 0);
            listLoader();
        }

        protected void jeloltContextMenuClick(object sender, RoutedEventArgs e)
        {
            MenuItem mitem = sender as MenuItem;
            JeloltListItems items = mitem.DataContext as JeloltListItems;

            if (mitem.Tag.ToString() == "delete")
            {
                MessageBoxResult result = MessageBox.Show("Elutasító E-Mail kiküldésre kerüljön?", "My App", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        pControl.jeloltKapcsDelete(items.id);
                        email.sendMail(items.email, emailTemplate.Elutasito_Email(items.nev));
                        break;
                    case MessageBoxResult.No:
                        pControl.jeloltKapcsDelete(items.id);
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                pControl.jeloltKapcsUpdate(items.id, Convert.ToInt32(mitem.Tag));
            }

            listLoader();
            formLoader();
        }

        protected void textBoxKeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            comment.add(comment_tartalom.Text, pControl.ProjektID, 0,0);
            listLoader();
            tbx.Text = "";
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

        protected void addPanelClose(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Hidden;
            formLoader();
        }

        private static int SelectedTabCode;
        public int selectedTabCode { get { return SelectedTabCode; } set { SelectedTabCode = value; } }

        protected void addPersonPanelOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Blur_Grid.Visibility = System.Windows.Visibility.Visible;
            projekt_kapcsolodo_grid.Visibility = System.Windows.Visibility.Visible;
            switch (btn.Tag)
            {
                case "jelolt":
                    {
                        selectedTabCode = 1;
                        projekt_kapcsolodo_list.ItemsSource = pControl.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
                        break;
                    }
                case "ertesitendo":
                    {
                        selectedTabCode = 2;
                        projekt_kapcsolodo_list.ItemsSource = pControl.Data_ErtesitendokCheckbox(Ember_Search_tbx.Text);
                        break;
                    }
                case "hr":
                    {
                        selectedTabCode = 3;
                        projekt_kapcsolodo_list.ItemsSource = pControl.Data_HrCheckbox(Ember_Search_tbx.Text);
                        break;
                    }
                default:
                    break;
            }
            formLoader();
        }

        protected void personPanelClose(object sender, RoutedEventArgs e)
        {
            Blur_Grid.Visibility = Visibility.Hidden;
            projekt_kapcsolodo_grid.Visibility = Visibility.Hidden;
        }

        protected void addPerson(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(selectedTabCode == 1)
            {
                SubJelolt items = btn.DataContext as SubJelolt;
                pControl.addJeloltInsert(items.id, pControl.ProjektID);
                projekt_kapcsolodo_list.ItemsSource = pControl.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
                kapcs_jeloltek_listBox.ItemsSource = pControl.Data_JeloltKapcs();
            }
            if (selectedTabCode == 2)
            {
                ertesitendok_struct items = btn.DataContext as ertesitendok_struct;
                pControl.addErtesitendokInsert(items.id);
                projekt_kapcsolodo_list.ItemsSource = pControl.Data_ErtesitendokCheckbox(Ember_Search_tbx.Text);
                kapcs_ertesitendo_listBox.ItemsSource = pControl.Data_ErtesitendokKapcs();
            }
            if (selectedTabCode == 3)
            {
                hr_struct items = btn.DataContext as hr_struct;
                pControl.addHrInsert(items.id);
                projekt_kapcsolodo_list.ItemsSource = pControl.Data_HrCheckbox(Ember_Search_tbx.Text);
                kapcs_hr_listBox.ItemsSource = pControl.Data_HrProject();
            }
        }

        protected void personSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            Ember_Search_Listbox.ItemsSource = pControl.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
        }

        protected void descriptionLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            string type = textbox.Tag.ToString();
            pControl.projectDescriptionUpdate(type, textbox.Text);
            formLoader();
        }

        protected void projectCost()
        {
            int sum = 0;
            List<koltsegek> list = pControl.Data_ProjectCost();

            foreach (var item in list)
            {
                sum += item.osszeg;
            }
            ossz_koltseg.Text = sum.ToString()+" ft";
        }

        protected void addCost(object sender, RoutedEventArgs e)
        {
            pControl.projectCostInsert(k_megnevezes_tbx.Text, k_osszeg_tbx.Text);
            koltseg_listBox.ItemsSource = pControl.Data_ProjectCost();
            projectCost();
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
        }

        protected void costPanelClose(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
            k_megnevezes_tbx.Text = "";
            k_osszeg_tbx.Text = "";
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void costPanelOpen(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Visible;
        }

        protected void deleteCost(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            koltsegek item = menu.DataContext as koltsegek;
            pControl.projectCostDelete(item.id);
            koltseg_listBox.ItemsSource = pControl.Data_ProjectCost();
            projectCost();
        }

        protected void publishChecked(object sender, RoutedEventArgs e)
        {
            pControl.publishProject(1);
        }

        protected void publishUnchecked(object sender, RoutedEventArgs e)
        {
            pControl.publishProject(0);
        }

        protected void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            JeloltListItems item = grid.DataContext as JeloltListItems;

            if (item.Checked == false)
            {
                item.Checked = true;
            }
            else
            {
                item.Checked = false;
            }
            kapcs_jeloltek_listBox.Items.Refresh();
        }
    }
}
