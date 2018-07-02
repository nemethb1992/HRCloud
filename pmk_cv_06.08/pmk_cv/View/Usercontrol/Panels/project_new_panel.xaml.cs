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

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_new_panel.xaml
    /// </summary>
    public partial class project_new_panel : UserControl
    {
        private Grid grid;
        private project_DataView project_DataView;
        projekt_cont pcontrol = new projekt_cont();
        applicant_cont acontrol = new applicant_cont();
        projekt_applicant_cont pacontrol = new projekt_applicant_cont();
        Session sess = new Session();
        public project_new_panel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkbox_loader();
        }
        private void checkbox_loader()
        {
            pc_cbx.ItemsSource = acontrol.PCataSource();
            vegzettseg_cbx.ItemsSource = acontrol.VegzettsegDataSource();
            nyelv_cbx.ItemsSource = acontrol.NyelvDataSource();
            munkakor_cbx.ItemsSource = acontrol.MunkakorDataSource();
            k1_cbx.ItemsSource = pacontrol.kompetencia_DataSource();
            k2_cbx.ItemsSource = pacontrol.kompetencia_DataSource();
            k3_cbx.ItemsSource = pacontrol.kompetencia_DataSource();
            k4_cbx.ItemsSource = pacontrol.kompetencia_DataSource();
            k5_cbx.ItemsSource = pacontrol.kompetencia_DataSource();
        }
        

        private List<ProjectInsertListItems> get_data_from_form()
        {
            
            ComboBox munkakorCBX = munkakor_cbx as ComboBox;
            munkakor_struct munkakor_items = munkakorCBX.SelectedItem as munkakor_struct;
            ComboBox nyelvCBX = nyelv_cbx as ComboBox;
            nyelv_struct nyelv_items = nyelvCBX.SelectedItem as nyelv_struct;
            ComboBox vegzettsegCBX = vegzettseg_cbx as ComboBox;
            vegzettseg_struct vegzettseg_items = vegzettsegCBX.SelectedItem as vegzettseg_struct;
            ComboBox pcCBX = pc_cbx as ComboBox;
            pc_struct pc_items = pcCBX.SelectedItem as pc_struct;

            ComboBox k1cbx = k1_cbx as ComboBox;
            kompetenciak k1item = k1cbx.SelectedItem as kompetenciak;
            ComboBox k2cbx = k2_cbx as ComboBox;
            kompetenciak k2item = k2cbx.SelectedItem as kompetenciak;
            ComboBox k3cbx = k3_cbx as ComboBox;
            kompetenciak k3item = k3cbx.SelectedItem as kompetenciak;
            ComboBox k4cbx = k4_cbx as ComboBox;
            kompetenciak k4item = k4cbx.SelectedItem as kompetenciak;
            ComboBox k5cbx = k5_cbx as ComboBox;
            kompetenciak k5item = k5cbx.SelectedItem as kompetenciak;

            DateTime localDate = DateTime.Now;
            List<ProjectInsertListItems> items = new List<ProjectInsertListItems>();

            items.Add(new ProjectInsertListItems
            {
                id = 0,
                hr_id = sess.UserData[0].id,
                megnevezes_projekt = nev_tbx.Text,
                pc = pc_items.id,
                vegzettseg = vegzettseg_items.id,
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                statusz = 1,
                fel_datum = localDate.ToString("yyyy.MM.dd."),
                le_datum = "-",
                nyelvtudas = nyelv_items.id,
                munkakor = munkakor_items.id,
                szuldatum = 0,
                ber = Convert.ToInt32(ber_tbx.Text),
                kepesseg1 = Convert.ToInt32(k1item.id),
                kepesseg2 = Convert.ToInt32(k2item.id),
                kepesseg3 = Convert.ToInt32(k3item.id),
                kepesseg4 = Convert.ToInt32(k4item.id),
                kepesseg5 = Convert.ToInt32(k5item.id)

            });
            return items;
        }
        private void projekt_INSERT_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pcontrol.Projekt_list_INSERT(get_data_from_form());
                grid.Children.Clear();
                grid.Children.Add(project_DataView = new project_DataView(grid));
            }
            catch (Exception)
            {
                MessageBox.Show("Nem lehet kitöltetlen mező!");
            }
        }

        private void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
