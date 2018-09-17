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

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        ControlSettings sControl = new ControlSettings();
        ControlApplicantProject paControl = new ControlApplicantProject();

        private Grid grid;

        public SettingsPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            listLoader();
        }
        protected void listLoader()
        {
            ertesitendok_editlist.ItemsSource = sControl.Ertesitendok_DataSource();
            vegzettsegek_editlist.ItemsSource = sControl.Vegzettseg_DataSource();
            munkakorok_editlist.ItemsSource = sControl.Munkakor_DataSource();
            pc_editlist.ItemsSource = sControl.PC_DataSource();
            ertesules_editlist.ItemsSource = sControl.Ertesulesek_DataSource();
            nyelv_editlist.ItemsSource = sControl.Nyelv_DataSource();
            kompetencia_editlist.ItemsSource = paControl.Data_Kompetencia();
        }

        protected void settingsInputGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "Új hozzáadása")
            {
                tbx.Text = "";
            }
        }

        protected void settingsInputLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = "Új hozzáadása";
            }
        }
        
        protected void vegzettseg(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    vegzettseg_struct items = menuItem.DataContext as vegzettseg_struct;
                    if (items.megnevezes_vegzettseg != "Összes")
                        sControl.settingDelete(items.id, "vegzettsegek");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    munkakor_struct items = menuItem.DataContext as munkakor_struct;
                    if (items.munkakor != "Összes")
                        sControl.settingDelete(items.id, "munkakor");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    pc_struct items = menuItem.DataContext as pc_struct;
                    if (items.megnevezes_pc != "Összes")
                        sControl.settingDelete(items.id, "pc");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ertesulesek items = menuItem.DataContext as ertesulesek;
                    if (items.ertesules_megnevezes != "Összes")
                        sControl.settingDelete(items.id, "ertesulesek");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    nyelv_struct items = menuItem.DataContext as nyelv_struct;
                    if (items.nyelv != "Összes")
                        sControl.settingDelete(items.id, "nyelv");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void kompetenciaDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    kompetenciak items = menuItem.DataContext as kompetenciak;
                    if (items.kompetencia_megnevezes != "Összes")
                        sControl.settingDelete(items.id, "kompetenciak");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        
        protected void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (vegzettsegek_new_tbx.Text != "" && vegzettsegek_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(vegzettsegek_new_tbx.Text, "vegzettsegek");
                vegzettsegek_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (munkakorok_new_tbx.Text != "" && munkakorok_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(munkakorok_new_tbx.Text, "munkakor");
                munkakorok_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (pc_new_tbx.Text != "" && pc_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(pc_new_tbx.Text, "pc");
                pc_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ertesules_new_tbx.Text != "" && ertesules_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(ertesules_new_tbx.Text, "ertesulesek");
                ertesules_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (nyelv_new_tbx.Text != "" && nyelv_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(nyelv_new_tbx.Text, "nyelv");
                nyelv_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }

        
        }

        protected void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (kompetencia_new_tbx.Text != "" && kompetencia_new_tbx.Text != "Új hozzáadása")
            {
                sControl.settingInsert(kompetencia_new_tbx.Text, "kompetenciak");
                kompetencia_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }
    }
}
