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
    /// Interaction logic for NewProjectPanel.xaml
    /// </summary>
    public partial class NewProjectPanel : UserControl
    {
        ControlProject pcontrol = new ControlProject();
        ControlApplicant acontrol = new ControlApplicant();
        ControlApplicantProject pacontrol = new ControlApplicantProject();
        Session sess = new Session();

        private Grid grid;
        private ProjectDataSheet project_DataView;

        public NewProjectPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkboxLoader();
        }

        protected void checkboxLoader()
        {
            pc_cbx.ItemsSource = acontrol.Data_Pc();
            vegzettseg_cbx.ItemsSource = acontrol.Data_Vegzettseg();
            nyelv_cbx.ItemsSource = acontrol.Data_Nyelv();
            munkakor_cbx.ItemsSource = acontrol.Data_Munkakor();
            k1_cbx.ItemsSource = pacontrol.Data_Kompetencia();
            k2_cbx.ItemsSource = pacontrol.Data_Kompetencia();
            k3_cbx.ItemsSource = pacontrol.Data_Kompetencia();
            k4_cbx.ItemsSource = pacontrol.Data_Kompetencia();
            k5_cbx.ItemsSource = pacontrol.Data_Kompetencia();
            if (pcontrol.Change == true)
            {
                uj_cim.Visibility = Visibility.Hidden;
                projekt_INSERT_btn.Visibility = Visibility.Hidden;
                modositas_cim.Visibility = Visibility.Visible;
                projekt_UPDATE_btn.Visibility = Visibility.Visible;
                modifyFormLoader();
            }
        }

        protected void modifyFormLoader()
        {
            List<ProjectExtendedListItems> li = pcontrol.Data_ProjectFull();
            nev_tbx.Text = li[0].megnevezes_projekt;
            tapasztalat_tbx.Text = li[0].tapasztalat_ev.ToString();
            ber_tbx.Text = li[0].ber.ToString();
            pc_cbx.SelectedIndex = checkboxCounter(acontrol.Data_Pc().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.pc, }));
            vegzettseg_cbx.SelectedIndex = checkboxCounter(acontrol.Data_Vegzettseg().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.vegzettseg, }));
            nyelv_cbx.SelectedIndex = checkboxCounter(acontrol.Data_Nyelv().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.nyelvtudas, }));
            munkakor_cbx.SelectedIndex = checkboxCounter(acontrol.Data_Munkakor().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.munkakor, }));
            k1_cbx.SelectedIndex = checkboxCounter(pacontrol.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg1, }));
            k2_cbx.SelectedIndex = checkboxCounter(pacontrol.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg2, }));
            k3_cbx.SelectedIndex = checkboxCounter(pacontrol.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg3, }));
            k4_cbx.SelectedIndex = checkboxCounter(pacontrol.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg4, }));
            k5_cbx.SelectedIndex = checkboxCounter(pacontrol.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg5, }));

        }

        int checkboxCounter(List<ComboBox_Seged_Struct> ossz_li, List<ComboBox_Seged_Struct> projekt_li)
        {
            int i = 0;
            foreach (var item in ossz_li)
            {
                if (item.id == projekt_li[0].id)
                {
                    break;
                }
                i++;
            }
            return i;
        }

        protected List<ProjectInsertListItems> getData()
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

        protected void projektInsertClick(object sender, RoutedEventArgs e)
        {
            pcontrol.Change = false;
            try
            {
                pcontrol.Projekt_list_INSERT(getData());
                grid.Children.Clear();
                grid.Children.Add(project_DataView = new ProjectDataSheet(grid));
        }
            catch (Exception)
            {
                MessageBox.Show("Nem lehet kitöltetlen mező!");
            }
        }

        protected void projektUpdateClick(object sender, RoutedEventArgs e)
        {
            pcontrol.Change = false;
            pcontrol.Projekt_list_UPDATE(getData());
            grid.Children.Clear();
            grid.Children.Add(project_DataView = new ProjectDataSheet(grid));
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
