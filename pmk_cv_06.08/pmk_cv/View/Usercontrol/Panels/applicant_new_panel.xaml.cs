﻿using HRCloud.Control;
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
    /// Interaction logic for applicant_new_panel.xaml
    /// </summary>
    public partial class applicant_new_panel : UserControl
    {
        private Grid grid;
        private applicant_DataView applicant_DataView;
        applicant_cont acontrol = new applicant_cont();
        projekt_cont pcontrol = new projekt_cont();
        public applicant_new_panel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkbox_loader();
        }
        private void checkbox_loader()
        {
            munkakor_cbx.ItemsSource = acontrol.MunkakorDataSource();
            munkakor2_cbx.ItemsSource = acontrol.MunkakorDataSource();
            munkakor3_cbx.ItemsSource = acontrol.MunkakorDataSource();
            vegzettseg_cbx.ItemsSource = acontrol.VegzettsegDataSource();
            nyelv_cbx.ItemsSource = acontrol.NyelvDataSource();
            nyelv2_cbx.ItemsSource = acontrol.NyelvDataSource();
            ertesules_cbx.ItemsSource = acontrol.ErtesulesekDataSource();
            neme_cbx.ItemsSource = acontrol.NemekDatasource();
            if (acontrol.Change == true)
            {
                uj_cim.Visibility = Visibility.Hidden;
                applicant_INSERT_btn.Visibility = Visibility.Hidden;
                modositas_cim.Visibility = Visibility.Visible;
                applicant_UPDATE_btn.Visibility = Visibility.Visible;
                LoadUp();
            }

        }
        public void LoadUp()
        {
            List<JeloltExtendedList> li = acontrol.JeloltFullDataSource();
            nev_tbx.Text = li[0].nev;
            email_tbx.Text = li[0].email;
            lakhely_tbx.Text = li[0].lakhely;
            telefon_tbx.Text = li[0].telefon;
            eletkor_tbx.Text = li[0].szuldatum.ToString();
            tapasztalat_tbx.Text = li[0].tapasztalat_ev.ToString();

            munkakor_cbx.SelectedIndex = cbx_counter(acontrol.MunkakorDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor, }));
            munkakor2_cbx.SelectedIndex = cbx_counter(acontrol.MunkakorDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor2, }));
            munkakor3_cbx.SelectedIndex = cbx_counter(acontrol.MunkakorDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_munkakor3, }));
            nyelv_cbx.SelectedIndex = cbx_counter(acontrol.NyelvDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_nyelvtudas, }));
            nyelv2_cbx.SelectedIndex = cbx_counter(acontrol.NyelvDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_nyelvtudas2, }));
            ertesules_cbx.SelectedIndex = cbx_counter(acontrol.ErtesulesekDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_ertesult, }));
            vegzettseg_cbx.SelectedIndex = cbx_counter(acontrol.VegzettsegDataSource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_vegz_terulet, }));
            neme_cbx.SelectedIndex = cbx_counter(acontrol.NemekDatasource().ConvertAll(x => new ComboBox_Seged_Struct { id = x.id, }), li.ConvertAll(x => new ComboBox_Seged_Struct { id = x.id_neme, }));


        }

        int cbx_counter(List<ComboBox_Seged_Struct> ossz_li, List<ComboBox_Seged_Struct> projekt_li)
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
        private List<JeloltExtendedList> get_data_from_form()
        {
            ComboBox ertesulesCBX = ertesules_cbx as ComboBox;
            ertesulesek ertesules_items = ertesulesCBX.SelectedItem as ertesulesek;
            ComboBox munkakorCBX = munkakor_cbx as ComboBox;
            munkakor_struct munkakor_items = munkakorCBX.SelectedItem as munkakor_struct;
            ComboBox munkakorCBX2 = munkakor2_cbx as ComboBox;
            munkakor_struct munkakor2_items = munkakorCBX2.SelectedItem as munkakor_struct;
            ComboBox munkakorCBX3 = munkakor3_cbx as ComboBox;
            munkakor_struct munkakor3_items = munkakorCBX3.SelectedItem as munkakor_struct;
            ComboBox nyelvCBX = nyelv_cbx as ComboBox;
            nyelv_struct nyelv_items = nyelvCBX.SelectedItem as nyelv_struct;
            ComboBox nyelvCBX2 = nyelv2_cbx as ComboBox;
            nyelv_struct nyelv2_items = nyelvCBX2.SelectedItem as nyelv_struct;
            ComboBox vegzettsegCBX = vegzettseg_cbx as ComboBox;
            vegzettseg_struct vegzettseg_items = vegzettsegCBX.SelectedItem as vegzettseg_struct;
            ComboBox nemeCBX = neme_cbx as ComboBox;
            neme_struct neme_items = neme_cbx.SelectedItem as neme_struct;

            DateTime localDate = DateTime.Now;
            List<JeloltExtendedList> items = new List<JeloltExtendedList>();
            items.Add(new JeloltExtendedList
            {
                id = 0,
                nev = nev_tbx.Text,
                email = email_tbx.Text,
                telefon = telefon_tbx.Text,
                //cim = cim_tbx.Text,
                lakhely = lakhely_tbx.Text,
                ertesult = ertesules_items.id.ToString(),
                szuldatum = Convert.ToInt32(eletkor_tbx.Text),
                neme = neme_items.id.ToString(),
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                munkakor = munkakor_items.id.ToString(),
                munkakor2 = munkakor2_items.id.ToString(),
                munkakor3 = munkakor3_items.id.ToString(),
                vegz_terulet = vegzettseg_items.id.ToString(),
                nyelvtudas = nyelv_items.id.ToString(),
                nyelvtudas2 = nyelv2_items.id.ToString(),
                //berigeny = Convert.ToInt32(ber_tbx.Text),
                reg_date = localDate.ToString("yyyy.MM.dd."),
            });
            return items;
        }
        private void applicant_INSERT_btn_Click(object sender, RoutedEventArgs e)
        {
                acontrol.Jelolt_list_INSERT(get_data_from_form());
                grid.Children.Clear();
                grid.Children.Add(applicant_DataView = new applicant_DataView(grid));
        }
        private void applicant_UPDATE_btn_Click(object sender, RoutedEventArgs e)
        {
            acontrol.Jelolt_list_UPDATE(get_data_from_form());
            grid.Children.Clear();
            grid.Children.Add(applicant_DataView = new applicant_DataView(grid));
        }
        private void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
