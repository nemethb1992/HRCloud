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

namespace HRCloud.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for Szakmai_interju_lista.xaml
    /// </summary>
    public partial class SzakmaiInterviewList : UserControl
    {
        ControlApplicantProject paControl = new ControlApplicantProject();
        ControlProject pControl = new ControlProject();
        ControlApplicant aControl = new ControlApplicant();
        ControlSzakmai szControl = new ControlSzakmai();

        private Grid grid;
        private SzakmaiKezdolap szakmaiKezdolap;
        private InterviewPanel interviewPanel;

        public SzakmaiInterviewList(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            bevont_interjuk_listbox.ItemsSource = szControl.Data_SzakmaiInterview();
        }

        protected void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiKezdolap = new SzakmaiKezdolap(grid));
        }

        protected void Szakmai_interju_open_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            interju_struct items = button.DataContext as interju_struct;

            paControl.InterjuID = items.id;
            pControl.ProjektID = items.projekt_id;
            aControl.ApplicantID = items.jelolt_id;

            grid.Children.Clear();
            grid.Children.Add(interviewPanel = new InterviewPanel(grid));
        }
    }
}
