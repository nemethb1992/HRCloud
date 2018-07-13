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
using HRCloud.Model;
using pmk_cv.Control;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_panel.xaml
    /// </summary>
    public partial class project_panel : UserControl
    {

        private static string HeaderSelecteds;
        public string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }


        private Grid sgrid;
        projekt_cont pcontrol = new projekt_cont();
        pageControl pageCont = new pageControl();
        private project_DataView project_DataView;
        private project_new_panel project_new_panel;
        public project_panel(Grid sgrid)
        {
            this.sgrid = sgrid;
            InitializeComponent();
            Checkbox_loader();
            Project_lister();
        }

        private void Checkbox_loader()
        {
            nyelv_srccbx.ItemsSource = pcontrol.NyelvDataSource();
            vegzettseg_srccbx.ItemsSource = pcontrol.VegzettsegDataSource();
        }

        List<string> searchbar_datalist()
        {
            ComboBox nyelv_cbx = nyelv_srccbx as ComboBox;
            ComboBox vegzettseg_cbx = vegzettseg_srccbx as ComboBox;
            nyelv_struct nyelv_item = nyelv_cbx.SelectedItem as nyelv_struct;
            vegzettseg_struct vegzettseg_item = vegzettseg_cbx.SelectedItem as vegzettseg_struct;

            string nyelvkStr = "";
            string vegzettsegStr = "";
            try  { if(vegzettseg_item.id !=0) vegzettsegStr = vegzettseg_item.id.ToString(); } catch (Exception) { }
            try  { if (nyelv_item.id != 0) nyelvkStr = nyelv_item.id.ToString(); } catch (Exception)  {}
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
            List<string> list = new List<string>();
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
        void Project_lister()
        {

                List<Projekt_Search_Memory> list = new List<Projekt_Search_Memory>();
                list.Add(new Projekt_Search_Memory() { statusz = 1 });
                pcontrol.projekt_search_memory = list;
                Button_color_changer();
                Button_color_changer();
            

            try
            {
                List<ProjectListItems> lista = pcontrol.ProjektListSource(searchbar_datalist());
                project_listBox.ItemsSource = lista;
                talalat_tbl.Text = "Találatok:  " + lista.Count.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //}
            //catch (Exception)
            //{
            //    if (vegzettseg_srccbx.SelectedIndex != 0)
            //        vegzettseg_srccbx.SelectedIndex = 0;
            //    if (nyelv_srccbx.SelectedIndex != 0)
            //        nyelv_srccbx.SelectedIndex = 0;
            //}

        }
        private void Button_color_changer()
        {
            var bc = new BrushConverter();
            if (pcontrol.projekt_search_memory[0].statusz == 1)
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
        private void Projekt_Open_btn(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ProjectListItems items = button.DataContext as ProjectListItems;
            pcontrol.ProjektID = items.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(project_DataView = new project_DataView(sgrid));
        }

        private void New_projekt_btn_Click(object sender, RoutedEventArgs e)
        {

            sgrid.Children.Clear();
            sgrid.Children.Add(project_new_panel = new project_new_panel(sgrid));
        }
        private void Projekt_Torles_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ProjectListItems items = menuItem.DataContext as ProjectListItems;
                    pcontrol.Projekt_delete(items.id);
                    Project_lister();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void Projekt_Archivalas_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan archiválni szeretnéd? \n\nArchiválás esetén, a kiválasztott projekt, passzív állapotba kerül\nés nem jelenik meg a weblapon.", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ProjectListItems items = menuItem.DataContext as ProjectListItems;
                    pcontrol.Projekt_Archiver(items.id, items.statusz);
                    Project_lister();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void projekt_passziv_btn_Click(object sender, RoutedEventArgs e)
        {
            pcontrol.Projekt_allapot_valto(0);
            project_listBox.ItemsSource = pcontrol.ProjektListSource(searchbar_datalist());
            Button_color_changer();
        }

        private void projekt_aktiv_btn_Click(object sender, RoutedEventArgs e)
        {
            pcontrol.Projekt_allapot_valto(1);
            project_listBox.ItemsSource = pcontrol.ProjektListSource(searchbar_datalist());
            Button_color_changer();
        }
        private void TextBox_PlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
                Tbx.Visibility = Visibility.Hidden;
        }
        private void TextBox_PlaceHolder_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
            {
                Tbx.Visibility = Visibility.Visible;
                textBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textBox.Foreground = Brushes.Black;
            }
        }

        private void Search_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Project_lister();
        }

        private void Combobox_selection(object sender, SelectionChangedEventArgs e)
        {
            Project_lister();
        }

        private void publikalt_check_Checked(object sender, RoutedEventArgs e)
        {
            Project_lister();
        }

        private void publikalt_check_Unchecked(object sender, RoutedEventArgs e)
        {
            Project_lister();
        }
        
        private void modositas_Click(object sender, RoutedEventArgs e)
        {
            pcontrol.Change = true;
            MenuItem item = sender as MenuItem;
            ProjectListItems itemSource = item.DataContext as ProjectListItems;
            pcontrol.ProjektID = itemSource.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(project_new_panel = new project_new_panel(sgrid));
        }

        private void Header_Click(object sender, MouseButtonEventArgs e)
        {
            Label item = sender as Label;
            HeaderSelected = item.Tag.ToString();
            Project_lister();
        }

        private void sorrend_check_Checked(object sender, RoutedEventArgs e)
        {
            Project_lister();
        }

        private void sorrend_check_Unchecked(object sender, RoutedEventArgs e)
        {
            Project_lister();
        }
    }
}










//private void btnShowSelectedItem_Click(object sender, RoutedEventArgs e)
//{
//    foreach (object o in lbTodoList.SelectedItems)
//        MessageBox.Show((o as ProjectListItems).projekt);
//}

//private void btnSelectLast_Click(object sender, RoutedEventArgs e)
//{
//    lbTodoList.SelectedIndex = lbTodoList.Items.Count - 1;
//}

//private void btnSelectNext_Click(object sender, RoutedEventArgs e)
//{
//    int nextIndex = 0;
//    if ((lbTodoList.SelectedIndex >= 0) && (lbTodoList.SelectedIndex < (lbTodoList.Items.Count - 1)))
//        nextIndex = lbTodoList.SelectedIndex + 1;
//    lbTodoList.SelectedIndex = nextIndex;
//}

//private void btnSelectCSharp_Click(object sender, RoutedEventArgs e)
//{
//    foreach (object o in lbTodoList.Items)
//    {
//        if ((o is ProjectListItems) && ((o as ProjectListItems).projekt.Contains("C#")))
//        {
//            lbTodoList.SelectedItem = o;
//            break;
//        }
//    }
//}

//private void btnSelectAll_Click(object sender, RoutedEventArgs e)
//{
//    foreach (object o in lbTodoList.Items)
//        lbTodoList.SelectedItems.Add(o);
//}