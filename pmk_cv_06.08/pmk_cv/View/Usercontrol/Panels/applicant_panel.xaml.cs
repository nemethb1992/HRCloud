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
using HRCloud.Control;
using HRCloud.Model;
using pmk_cv.Control;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for applicant_panel.xaml
    /// </summary>
    public partial class applicant_panel : UserControl
    {
        applicant_cont acontrol = new applicant_cont();
        private applicant_DataView applicant_DataView;
        private applicant_new_panel applicant_new_panel;
        pageControl pageCont = new pageControl();
        private Grid sgrid;
        public applicant_panel(Grid sgrid)
        {
            this.sgrid = sgrid;
            InitializeComponent();
            startMethods();
        }
        private void startMethods()
        {
            cbxload();
            Applicant_lister();
        }

        List<string> searchbar_datalist()
        {
            List<string> list = new List<string>();
            ComboBox munkakor = munkakor_srccbx as ComboBox;
            ComboBox vegzettseg = vegzettseg_srccbx as ComboBox;
            ComboBox nemek = nemek_srccbx as ComboBox;

            munkakor_struct munkakor_item = munkakor.SelectedItem as munkakor_struct;
            vegzettseg_struct vegzettseg_item = vegzettseg.SelectedItem as vegzettseg_struct;
            neme_struct nemek_item = nemek.SelectedItem as neme_struct;

            string munkakorStr = "";
            if (munkakor.SelectedIndex != 0)
            {
                try
                {
                    munkakorStr = munkakor_item.id.ToString();
                }
                catch (Exception)
                {
                }
            }
            string vegzettsegStr = "";
            if (vegzettseg.SelectedIndex != 0)
            {
                try
                {
                    vegzettsegStr = vegzettseg_item.id.ToString();
                }
                catch (Exception)
                {
                }
            }
            string nemekStr = "";
            if (nemek.SelectedIndex != 0)
            {
                try
                {
                    nemekStr = nemek_item.id.ToString();
                }
                catch (Exception)
                {
                }
            }
            string tapasztalat = tapsztalat_srcinp.Text;
            if (tapsztalat_srcinp.Text == "")
                tapasztalat = "";
            string interjuk = interju_srcinp.Text;
            if (interju_srcinp.Text == "")
                interjuk = "";
            string szabad = "";
            if (szabad_check.IsChecked == true)
                szabad = "1";

            list.Add(nev_srcinp.Text);
            list.Add(lakhely_srcinp.Text);
            list.Add(email_srcinp.Text);
            list.Add(eletkor_srcinp.Text);
            list.Add(tapasztalat);
            list.Add(regdate_srcinp.Text);
            list.Add(interjuk);
            list.Add(nemekStr);
            list.Add(munkakorStr);
            list.Add(vegzettsegStr);
            list.Add(cimke_srcinp.Text);
            list.Add(szabad);



            return list;
        }

        // TODO: Azt a try-os szart kiküszöbölni, a projektes mintájára

        void Applicant_lister()
        {
                List<JeloltListItems> lista = acontrol.JeloltListSource(searchbar_datalist());
                applicant_listBox.ItemsSource = lista;
                talalat_tbl.Text = "Találatok:  " + lista.Count.ToString();
            
        }

        void cbxload()
        {
            vegzettseg_srccbx.ItemsSource = acontrol.VegzettsegDataSource();
            munkakor_srccbx.ItemsSource = acontrol.MunkakorDataSource();
            nemek_srccbx.ItemsSource = acontrol.NemekDatasource();
        }

        private void applicant_open_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;
            acontrol.ApplicantID = items.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_DataView = new applicant_DataView(sgrid));
        }
        private void applicant_delete_context_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    JeloltListItems items = menuItem.DataContext as JeloltListItems;
                    acontrol.Jelolt_delete(items.id);
                    Applicant_lister();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

        }
        private void New_applicant_btn_Click(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_new_panel = new applicant_new_panel(sgrid));

        }
        private void Search_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Applicant_lister();
        }

        private void app_eletkor_tbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SearchCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Applicant_lister();
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
        private void SearchBar_Refresh(object sender, RoutedEventArgs e)
        {
            TextBox nev_tbx = nev_srcinp as TextBox;
            munkakor_srccbx.SelectedIndex = 0;
            vegzettseg_srccbx.SelectedIndex = 0;
            nemek_srccbx.SelectedIndex = 0;
            nev_srcinp.Text = "";
            lakhely_srcinp.Text = "";
            email_srcinp.Text = "";
            eletkor_srcinp.Text = "";
            tapsztalat_srcinp.Text = "";
            regdate_srcinp.Text = "";
            interju_srcinp.Text = "";
        }

        private void szabad_check_Checked(object sender, RoutedEventArgs e)
        {
            Applicant_lister();
        }

        private void szabad_check_Unchecked(object sender, RoutedEventArgs e)
        {
            Applicant_lister();
        }
    }

}
