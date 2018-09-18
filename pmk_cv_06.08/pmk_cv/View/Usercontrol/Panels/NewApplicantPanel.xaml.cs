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
    /// Interaction logic for applicant_new_panel.xaml
    /// </summary>
    public partial class NewApplicantPanel : UserControl
    {
        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();

        private Grid grid;
        private ApplicantDataSheet applicantDataSheet;

        public NewApplicantPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkboxLoader();
        }

        protected void checkboxLoader()
        {
            munkakor_cbx.ItemsSource = aControl.Data_Munkakor();
            munkakor2_cbx.ItemsSource = aControl.Data_Munkakor();
            munkakor3_cbx.ItemsSource = aControl.Data_Munkakor();
            vegzettseg_cbx.ItemsSource = aControl.Data_Vegzettseg();
            nyelv_cbx.ItemsSource = aControl.Data_Nyelv();
            nyelv2_cbx.ItemsSource = aControl.Data_Nyelv();
            ertesules_cbx.ItemsSource = aControl.Data_Ertesulesek();
            neme_cbx.ItemsSource = aControl.Data_Nemek();

            if (aControl.Change == true)
            {
                uj_cim.Visibility = Visibility.Hidden;
                applicant_INSERT_btn.Visibility = Visibility.Hidden;
                modositas_cim.Visibility = Visibility.Visible;
                applicant_UPDATE_btn.Visibility = Visibility.Visible;
                modifyFormLoader();
            }
        }

        protected void modifyFormLoader()
        {
            List<JeloltExtendedList> li = aControl.Data_JeloltFull();
            nev_tbx.Text = li[0].nev;
            email_tbx.Text = li[0].email;
            lakhely_tbx.Text = li[0].lakhely;
            telefon_tbx.Text = li[0].telefon;
            eletkor_tbx.Text = li[0].szuldatum.ToString();
            tapasztalat_tbx.Text = li[0].tapasztalat_ev.ToString();

            munkakor_cbx.SelectedIndex = checkboxCounter(aControl.Data_Munkakor().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor, }));
            munkakor2_cbx.SelectedIndex = checkboxCounter(aControl.Data_Munkakor().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor2, }));
            munkakor3_cbx.SelectedIndex = checkboxCounter(aControl.Data_Munkakor().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor3, }));
            nyelv_cbx.SelectedIndex = checkboxCounter(aControl.Data_Nyelv().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_nyelvtudas, }));
            nyelv2_cbx.SelectedIndex = checkboxCounter(aControl.Data_Nyelv().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_nyelvtudas2, }));
            ertesules_cbx.SelectedIndex = checkboxCounter(aControl.Data_Ertesulesek().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_ertesult, }));
            vegzettseg_cbx.SelectedIndex = checkboxCounter(aControl.Data_Vegzettseg().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_vegz_terulet, }));
            neme_cbx.SelectedIndex = checkboxCounter(aControl.Data_Nemek().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_neme, }));
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

        protected List<JeloltExtendedList> getFormData()
        {
            DateTime localDate = DateTime.Now;
            List<JeloltExtendedList> items = new List<JeloltExtendedList>();

            neme_struct nemeComboBoxItem = (neme_cbx as ComboBox).SelectedItem as neme_struct;
            nyelv_struct nyelvComboBoxItem = (nyelv_cbx as ComboBox).SelectedItem as nyelv_struct;
            nyelv_struct nyelv2ComboBoxItem = (nyelv2_cbx as ComboBox).SelectedItem as nyelv_struct;
            ertesulesek ertesulesComboBoxItem = (ertesules_cbx as ComboBox).SelectedItem as ertesulesek;
            munkakor_struct munkakorComboBoxItem = (munkakor_cbx as ComboBox).SelectedItem as munkakor_struct;
            munkakor_struct munkakor2ComboBoxItem = (munkakor2_cbx as ComboBox).SelectedItem as munkakor_struct;
            munkakor_struct munkakor3ComboBoxItem = (munkakor3_cbx as ComboBox).SelectedItem as munkakor_struct;
            vegzettseg_struct vegzettsegComboBoxItem = (vegzettseg_cbx as ComboBox).SelectedItem as vegzettseg_struct;
            
            items.Add(new JeloltExtendedList
            {
                id = 0,
                nev = nev_tbx.Text,
                email = email_tbx.Text,
                telefon = telefon_tbx.Text,
                lakhely = lakhely_tbx.Text,
                ertesult = ertesulesComboBoxItem.id.ToString(),
                szuldatum = Convert.ToInt32(eletkor_tbx.Text),
                neme = nemeComboBoxItem.id.ToString(),
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                munkakor = munkakorComboBoxItem.id.ToString(),
                munkakor2 = munkakor2ComboBoxItem.id.ToString(),
                munkakor3 = munkakor3ComboBoxItem.id.ToString(),
                vegz_terulet = vegzettsegComboBoxItem.id.ToString(),
                nyelvtudas = nyelvComboBoxItem.id.ToString(),
                nyelvtudas2 = nyelv2ComboBoxItem.id.ToString(),
                reg_date = localDate.ToString("yyyy.MM.dd."),
            });
            return items;
        }

        protected void applicantInsertClick(object sender, RoutedEventArgs e)
        {
            aControl.applicantInsert(getFormData());
            grid.Children.Clear();
            grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
        }

        protected void applicantModifyClick(object sender, RoutedEventArgs e)
        {
            aControl.applicantUpdate(getFormData());
            grid.Children.Clear();
            grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

//combobox = neme_cbx as ComboBox;
//            neme_struct nemeComboBoxItem = combobox.SelectedItem as neme_struct;
//combobox = nyelv_cbx as ComboBox;
//            nyelv_struct nyelvComboBoxItem = combobox.SelectedItem as nyelv_struct;
//combobox = nyelv2_cbx as ComboBox;
//            nyelv_struct nyelv2ComboBoxItem = combobox.SelectedItem as nyelv_struct;
//combobox = ertesules_cbx as ComboBox;
//            ertesulesek ertesulesComboBoxItem = combobox.SelectedItem as ertesulesek;
//combobox = munkakor_cbx as ComboBox;
//            munkakor_struct munkakorComboBoxItem = combobox.SelectedItem as munkakor_struct;
//combobox = munkakor2_cbx as ComboBox;
//            munkakor_struct munkakor2ComboBoxItem = combobox.SelectedItem as munkakor_struct;
//combobox = munkakor3_cbx as ComboBox;
//            munkakor_struct munkakor3ComboBoxItem = combobox.SelectedItem as munkakor_struct;
//combobox = vegzettseg_cbx as ComboBox;
//            vegzettseg_struct vegzettsegComboBoxItem = combobox.SelectedItem as vegzettseg_struct;
