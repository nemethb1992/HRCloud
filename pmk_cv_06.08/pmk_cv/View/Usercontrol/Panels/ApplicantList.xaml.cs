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
        PageControl pageCont = new PageControl();

        private ApplicantDataSheet applicantDataSheet;
        private NewApplicantPanel newApplicantPanel;
        private Grid sgrid;

        public ApplicantList(Grid sgrid)
        {
            this.sgrid = sgrid;
            InitializeComponent();
            startMethods();
        }
        protected void startMethods()
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

        protected void applicantListLoader()
        {
                List<JeloltListItems> list = acontrol.applicantList(searchValues());
                applicant_listBox.ItemsSource = list;
                talalat_tbl.Text = "Találatok:  " + list.Count.ToString();
            
        }

        protected void checkBoxLoader()
        {
            vegzettseg_srccbx.ItemsSource = acontrol.Data_Vegzettseg();
            munkakor_srccbx.ItemsSource = acontrol.Data_Munkakor();
            nemek_srccbx.ItemsSource = acontrol.Data_Nemek();
        }

        protected void applicantOpenClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            JeloltListItems items = button.DataContext as JeloltListItems;
            acontrol.ApplicantID = items.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(applicantDataSheet = new ApplicantDataSheet(sgrid));
        }
        protected void applicantDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    JeloltListItems items = menuItem.DataContext as JeloltListItems;
                    acontrol.applicantFullDelete(items.id);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

        }
        protected void navigateToNewApplicantPanel(object sender, RoutedEventArgs e)
        {
            sgrid.Children.Clear();
            sgrid.Children.Add(newApplicantPanel = new NewApplicantPanel(sgrid));

        }
        protected void searchInputTextChanged(object sender, TextChangedEventArgs e)
        {
            applicantListLoader();
        }

        protected void numericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void searchCbxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            applicantListLoader();
        }
        protected void textBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            string textboxName = ((TextBox)sender).Tag.ToString();
            var textboxElement = (TextBox)this.FindName(textboxName);

            if (((TextBox)sender).Text == "")
                textboxElement.Visibility = Visibility.Hidden;
        }
        protected void textBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            string textboxName = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(textboxName);

            if (((TextBox)sender).Text == "")
            {
                Tbx.Visibility = Visibility.Visible;
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textbox.Foreground = Brushes.Black;
            }
        }
        protected void searchBarRefresh(object sender, RoutedEventArgs e)
        {
            TextBox nev_tbx = nev_srcinp as TextBox;  // TODO: <- ez kell?
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

        protected void szabadChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void szabadUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }
        protected void modositasClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            JeloltListItems itemSource = item.DataContext as JeloltListItems;
            acontrol.Change = true;
            acontrol.ApplicantID = itemSource.id;
            sgrid.Children.Clear();
            sgrid.Children.Add(newApplicantPanel = new NewApplicantPanel(sgrid));
        }

        protected void headerClick(object sender, MouseButtonEventArgs e)
        {
            Label item = sender as Label;
            HeaderSelected = item.Tag.ToString();
            applicantListLoader();
        }

        protected void sorrendChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }
    }

}
