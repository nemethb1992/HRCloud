using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HRCloud.Control;
using HRCloud.Model;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_panel.xaml
    /// </summary>
    public partial class ProjectList : UserControl
    {
        private static string HeaderSelecteds;
        protected string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }

        ControlProject pControl = new ControlProject();

        private ProjectDataSheet projectDataSheet;
        private NewProjectPanel newProjectPanel;
        private Grid grid;

        public ProjectList(Grid grid)
        {
            this.grid = grid;
            this.DataContext = projectListLoader2();
            InitializeComponent();
            //project_listBox.Items.Refresh();
            checkBoxLoader();
            //projectListLoader();
        }

        protected void checkBoxLoader()
        {
            nyelv_srccbx.ItemsSource = pControl.Data_Nyelv();
            vegzettseg_srccbx.ItemsSource = pControl.Data_Vegzettseg();
        }

        protected List<string> getSearchData()
        {
            List<string> list = new List<string>();
            nyelv_struct nyelvItem = null;
            vegzettseg_struct vegzettsegItem = null;
            string nyelvkStr = "";
            string vegzettsegStr = "";

            try
            {
                nyelvItem = (nyelv_srccbx as ComboBox).SelectedItem as nyelv_struct;
                vegzettsegItem = (vegzettseg_srccbx as ComboBox).SelectedItem as vegzettseg_struct;
            }
            catch (Exception){
            }

            try  { if(vegzettsegItem.id !=-1) vegzettsegStr = vegzettsegItem.id.ToString(); } catch (Exception) { }

            try  { if (nyelvItem.id != -1) nyelvkStr = nyelvItem.id.ToString(); } catch (Exception)  {}

            string jeloltszam = jeloltszam_srcinp.Text;
            if (jeloltszam_srcinp.Text == "")
                jeloltszam = "0";

            string interjuk = interju_srcinp.Text;
            if (interju_srcinp.Text == "")
                interjuk = "0";

            string publikalt = "";
            if (publikalt_check.IsChecked == true)
                publikalt = "1";

            string sorrend = " ASC";
            if (sorrend_check.IsChecked == true)
                sorrend = " DESC";

            list.Add(projektnev_srcinp.Text);
            list.Add(jeloltszam);
            list.Add(publikalva_srcinp.Text);
            list.Add(interjuk);
            list.Add(pc_srcinp.Text);
            list.Add(nyelvkStr);
            list.Add(vegzettsegStr);
            list.Add(cimke_srcinp.Text);
            list.Add(jeloltnev_srcinp.Text);
            list.Add(publikalt);
            list.Add(HeaderSelected);
            list.Add(sorrend);

            return list;
        }

        protected void projectListLoader()
        {
            List<Projekt_Search_Memory> list = new List<Projekt_Search_Memory>();
            
            list.Add(new Projekt_Search_Memory() { statusz = 1 });
            pControl.projectSearchMemory = list;
            buttonColorChange();

            try{
                List<ProjectListItems> lista = pControl.Data_ProjectFull(getSearchData());
                talalat_tbl.Text = "Találatok:  " + lista.Count.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        protected List<ProjectListItems> projectListLoader2()
        {
            List<Projekt_Search_Memory> list = new List<Projekt_Search_Memory>();
            List<ProjectListItems> lista = new List<ProjectListItems>();

            list.Add(new Projekt_Search_Memory() { statusz = 1 });
            pControl.projectSearchMemory = list;
            //buttonColorChange();
            lista = pControl.Data_ProjectFull(getSearchData());
            return lista;
        }

        protected void buttonColorChange()
        {
            var bc = new BrushConverter();
            if (pControl.projectSearchMemory[0].statusz == 1)
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_aktiv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                projekt_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
            }
            else
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                projekt_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_passziv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
            }
        }

        protected void projectOpenClick(object sender, RoutedEventArgs e)
        {
            ProjectListItems items = (sender as Button).DataContext as ProjectListItems;
            pControl.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
        }

        protected void New_projekt_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(newProjectPanel = new NewProjectPanel(grid));
        }

        protected void projectDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ProjectListItems items = (sender as MenuItem).DataContext as ProjectListItems;
                    pControl.projectDelete(items.id);
                    projectListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void projectArchivateClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan archiválni szeretnéd? \n\nArchiválás esetén, a kiválasztott projekt, passzív állapotba kerül\nés nem jelenik meg a weblapon.", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ProjectListItems items = (sender as MenuItem).DataContext as ProjectListItems;
                    pControl.projectArchiver(items.id, items.statusz);
                    projectListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void projectPassivateClick(object sender, RoutedEventArgs e)
        {
            pControl.statusChange(0);
            project_listBox.ItemsSource = pControl.Data_ProjectFull(getSearchData());
            buttonColorChange();
        }

        protected void projectActivateClick(object sender, RoutedEventArgs e)
        {
            pControl.statusChange(1);
            project_listBox.ItemsSource = pControl.Data_ProjectFull(getSearchData());
            buttonColorChange();
        }

        protected void textBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
                Tbx.Visibility = Visibility.Hidden;
        }

        protected void textBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            TextBox textboxOther = (TextBox)this.FindName(textbox.Tag.ToString());

            if (((TextBox)sender).Text == "")
            {
                textboxOther.Visibility = Visibility.Visible;
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textbox.Foreground = Brushes.Black;
            }
        }

        protected async void searchInputTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(500);
            if (fisrtLength == textbox.Text.Length)
                projectListLoader();
        }

        protected void comboboxSelection(object sender, SelectionChangedEventArgs e)
        {
            projectListLoader();
        }

        protected void publikaltChecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }

        protected void publikaltUnchecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }

        protected void modositasClick(object sender, RoutedEventArgs e)
        {
            pControl.Change = true;
            ProjectListItems itemSource = (sender as MenuItem).DataContext as ProjectListItems;

            pControl.ProjektID = itemSource.id;
            grid.Children.Clear();
            grid.Children.Add(newProjectPanel = new NewProjectPanel(grid));
        }

        protected void headerClick(object sender, MouseButtonEventArgs e)
        {
            HeaderSelected = (sender as Label).Tag.ToString();
            projectListLoader();
        }

        protected void sorrendChecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }

        protected void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }
    }
}