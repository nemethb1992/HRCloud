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
    public partial class ApplicantList : UserControl
    {
        private static string HeaderSelecteds;
        public string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }


        ControlApplicant acontrol = new ControlApplicant();
        private ApplicantDataSheet applicant_DataView;
        private NewApplicantPanel applicant_new_panel;
        PageControl pageCont = new PageControl();
        private Grid sgrid;

        public ApplicantList(Grid sgrid)
        {
            this.sgrid = sgrid;
            InitializeComponent();
            startMethods();
        }
        private void startMethods()
        {
            checkBoxLoader();
            applicantListLoader();
        }

        List<string> searchValues() 
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

            string sorrend = " ASC";

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
            list.Add(HeaderSelected);
            list.Add(sorrend);

            return list;
        }
        
        void applicantListLoader()
        {
                List<JeloltListItems> list = acontrol.applicantList(searchValues());
                applicant_listBox.ItemsSource = list;
                talalat_tbl.Text = "Találatok:  " + list.Count.ToString();
            
        }

        void checkBoxLoader()
        {
            vegzettseg_srccbx.ItemsSource = acontrol.VegzettsegDataSource();
            munkakor_srccbx.ItemsSource = acontrol.MunkakorDataSource();
            nemek_srccbx.ItemsSource = acontrol.NemekDatasource();
        }

        private void applicantOpenClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;
            acontrol.ApplicantID = items.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_DataView = new ApplicantDataSheet(sgrid));
        }
        private void applicantDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    JeloltListItems items = menuItem.DataContext as JeloltListItems;
                    acontrol.Jelolt_delete(items.id);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

        }
        private void navigateToNewApplicantPanel(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_new_panel = new NewApplicantPanel(sgrid));

        }
        private void searchInputTextChanged(object sender, TextChangedEventArgs e)
        {
            applicantListLoader();
        }

        private void numericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void searchCbxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            applicantListLoader();
        }
        private void textBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
                Tbx.Visibility = Visibility.Hidden;
        }
        private void textBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
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
        private void searchBarRefresh(object sender, RoutedEventArgs e)
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

        private void szabadChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        private void szabadUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }
        private void modositasClick(object sender, RoutedEventArgs e)
        {
            acontrol.Change = true;
            MenuItem item = sender as MenuItem;
            JeloltListItems itemSource = item.DataContext as JeloltListItems;
            acontrol.ApplicantID = itemSource.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(applicant_new_panel = new NewApplicantPanel(sgrid));
        }

        private void headerClick(object sender, MouseButtonEventArgs e)
        {
            Label item = sender as Label;
            HeaderSelected = item.Tag.ToString();
            applicantListLoader();
        }

        private void sorrendChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        private void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }
    }

}
