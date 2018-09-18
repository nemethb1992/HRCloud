using HRCloud.Control;
using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRCloud.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for NewProjectPanel.xaml
    /// </summary>
    public partial class NewProjectPanel : UserControl
    {
        ControlProject pControl = new ControlProject();
        ControlApplicant aControl = new ControlApplicant();
        ControlApplicantProject paControl = new ControlApplicantProject();
        Session session = new Session();

        private Grid grid;
        private ProjectDataSheet projectDataSheet;

        public NewProjectPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkboxLoader();
        }

        protected void checkboxLoader()
        {
            pc_cbx.ItemsSource = aControl.Data_Pc();
            vegzettseg_cbx.ItemsSource = aControl.Data_Vegzettseg();
            nyelv_cbx.ItemsSource = aControl.Data_Nyelv();
            munkakor_cbx.ItemsSource = aControl.Data_Munkakor();
            k1_cbx.ItemsSource = paControl.Data_Kompetencia();
            k2_cbx.ItemsSource = paControl.Data_Kompetencia();
            k3_cbx.ItemsSource = paControl.Data_Kompetencia();
            k4_cbx.ItemsSource = paControl.Data_Kompetencia();
            k5_cbx.ItemsSource = paControl.Data_Kompetencia();

            if (pControl.Change == true)
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
            List<ProjectExtendedListItems> list = pControl.Data_ProjectFull();
            nev_tbx.Text = list[0].megnevezes_projekt;
            tapasztalat_tbx.Text = list[0].tapasztalat_ev.ToString();
            ber_tbx.Text = list[0].ber.ToString();
            pc_cbx.SelectedIndex = checkboxCounter(aControl.Data_Pc().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.pc, }));
            vegzettseg_cbx.SelectedIndex = checkboxCounter(aControl.Data_Vegzettseg().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.vegzettseg, }));
            nyelv_cbx.SelectedIndex = checkboxCounter(aControl.Data_Nyelv().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.nyelvtudas, }));
            munkakor_cbx.SelectedIndex = checkboxCounter(aControl.Data_Munkakor().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.munkakor, }));
            k1_cbx.SelectedIndex = checkboxCounter(paControl.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg1, }));
            k2_cbx.SelectedIndex = checkboxCounter(paControl.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg2, }));
            k3_cbx.SelectedIndex = checkboxCounter(paControl.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg3, }));
            k4_cbx.SelectedIndex = checkboxCounter(paControl.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg4, }));
            k5_cbx.SelectedIndex = checkboxCounter(paControl.Data_Kompetencia().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), list.ConvertAll(x => new ComboBox_Seged_Struct { id = x.kepesseg5, }));
        }

        protected int checkboxCounter(List<ComboBox_Seged_Struct> ossz_li, List<ComboBox_Seged_Struct> projekt_li)
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
            vegzettseg_struct vegzettsegComboBox = (vegzettseg_cbx as ComboBox).SelectedItem as vegzettseg_struct;
            munkakor_struct munkakorComboBox = (munkakor_cbx as ComboBox).SelectedItem as munkakor_struct;
            nyelv_struct nyelvComboBox = (nyelv_cbx as ComboBox).SelectedItem as nyelv_struct;
            kompetenciak kepzettseg1ComboBox = (k1_cbx as ComboBox).SelectedItem as kompetenciak;
            kompetenciak kepzettseg2ComboBox = (k2_cbx as ComboBox).SelectedItem as kompetenciak;
            kompetenciak kepzettseg3ComboBox = (k3_cbx as ComboBox).SelectedItem as kompetenciak;
            kompetenciak kepzettseg4ComboBox = (k4_cbx as ComboBox).SelectedItem as kompetenciak;
            kompetenciak kepzettseg5ComboBox = (k5_cbx as ComboBox).SelectedItem as kompetenciak;
            pc_struct pcComboBox = (pc_cbx as ComboBox).SelectedItem as pc_struct;
            DateTime localDate = DateTime.Now;
            List<ProjectInsertListItems> items = new List<ProjectInsertListItems>();

            items.Add(new ProjectInsertListItems
            {
                id = 0,
                hr_id = session.UserData[0].id,
                megnevezes_projekt = nev_tbx.Text,
                pc = pcComboBox.id,
                vegzettseg = vegzettsegComboBox.id,
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                statusz = 1,
                fel_datum = localDate.ToString("yyyy.MM.dd."),
                le_datum = "-",
                nyelvtudas = nyelvComboBox.id,
                munkakor = munkakorComboBox.id,
                szuldatum = 0,
                ber = Convert.ToInt32(ber_tbx.Text),
                kepesseg1 = Convert.ToInt32(kepzettseg1ComboBox.id),
                kepesseg2 = Convert.ToInt32(kepzettseg2ComboBox.id),
                kepesseg3 = Convert.ToInt32(kepzettseg3ComboBox.id),
                kepesseg4 = Convert.ToInt32(kepzettseg4ComboBox.id),
                kepesseg5 = Convert.ToInt32(kepzettseg5ComboBox.id)

            });
            return items;
        }

        protected void projektInsertClick(object sender, RoutedEventArgs e)
        {
            pControl.Change = false;
            try
            {
                pControl.projectInsert(getData());
                grid.Children.Clear();
                grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
            }
            catch (Exception)
            {
                MessageBox.Show("Nem lehet kitöltetlen mező!");
            }
        }

        protected void projektUpdateClick(object sender, RoutedEventArgs e)
        {
            pControl.Change = false;
            pControl.projectUpdate(getData());
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
