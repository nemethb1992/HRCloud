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
        ControlSettings scontrol = new ControlSettings();
        ControlApplicantProject pa_control = new ControlApplicantProject();
        private Grid grid;
        public SettingsPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            list_loader();
            //scontrol.kompetenciaíró();
        }
        protected void list_loader()
        {
            ertesitendok_editlist.ItemsSource = scontrol.Ertesitendok_DataSource();
            vegzettsegek_editlist.ItemsSource = scontrol.Vegzettseg_DataSource();
            munkakorok_editlist.ItemsSource = scontrol.Munkakor_DataSource();
            pc_editlist.ItemsSource = scontrol.PC_DataSource();
            ertesules_editlist.ItemsSource = scontrol.Ertesulesek_DataSource();
            nyelv_editlist.ItemsSource = scontrol.Nyelv_DataSource();
            kompetencia_editlist.ItemsSource = pa_control.Data_Kompetencia();
        }

        protected void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Text == "Új hozzáadása")
            {
                tbx.Text = "";
            }
        }

        protected void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Text == "")
            {
                tbx.Text = "Új hozzáadása";
            }
        }


        protected void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    vegzettseg_struct items = menuItem.DataContext as vegzettseg_struct;
                    if (items.megnevezes_vegzettseg != "Összes")
                        scontrol.item_delete(items.id, "vegzettsegek");
                    list_loader();
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
                        scontrol.item_delete(items.id, "munkakor");
                    list_loader();
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
                        scontrol.item_delete(items.id, "pc");
                    list_loader();
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
                        scontrol.item_delete(items.id, "ertesulesek");
                    list_loader();
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
                        scontrol.item_delete(items.id, "nyelv");
                    list_loader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        protected void kompetencia_delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    kompetenciak items = menuItem.DataContext as kompetenciak;
                    if (items.kompetencia_megnevezes != "Összes")
                        scontrol.item_delete(items.id, "kompetenciak");
                    list_loader();
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
                scontrol.item_insert(vegzettsegek_new_tbx.Text, "vegzettsegek");
                vegzettsegek_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }
        }

        protected void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (munkakorok_new_tbx.Text != "" && munkakorok_new_tbx.Text != "Új hozzáadása")
            {
                scontrol.item_insert(munkakorok_new_tbx.Text, "munkakor");
                munkakorok_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }
        }

        protected void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (pc_new_tbx.Text != "" && pc_new_tbx.Text != "Új hozzáadása")
            {
                scontrol.item_insert(pc_new_tbx.Text, "pc");
                pc_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }
        }

        protected void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ertesules_new_tbx.Text != "" && ertesules_new_tbx.Text != "Új hozzáadása")
            {
                scontrol.item_insert(ertesules_new_tbx.Text, "ertesulesek");
                ertesules_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }
        }

        protected void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (nyelv_new_tbx.Text != "" && nyelv_new_tbx.Text != "Új hozzáadása")
            {
                scontrol.item_insert(nyelv_new_tbx.Text, "nyelv");
                nyelv_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }

        
        }

        protected void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (kompetencia_new_tbx.Text != "" && kompetencia_new_tbx.Text != "Új hozzáadása")
            {
                scontrol.item_insert(kompetencia_new_tbx.Text, "kompetenciak");
                kompetencia_new_tbx.Text = "Új hozzáadása";
                list_loader();
            }


        }
    }
}
