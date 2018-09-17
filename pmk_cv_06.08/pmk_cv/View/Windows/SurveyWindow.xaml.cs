using HRCloud.View.Usercontrol.Surveys;
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
using System.Windows.Shapes;

namespace HRCloud.View.Windows
{
    /// <summary>
    /// Interaction logic for Survey_Window.xaml
    /// </summary>
    public partial class SurveyWindow : Window
    {
        private FirstRegistration firstRegistration;
        public SurveyWindow()
        {
            InitializeComponent();
            SwitchGrid.Children.Add(firstRegistration = new FirstRegistration(SwitchGrid));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
