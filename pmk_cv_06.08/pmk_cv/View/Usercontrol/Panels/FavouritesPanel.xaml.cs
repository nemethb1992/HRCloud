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
    /// Interaction logic for favourites_panel.xaml
    /// </summary>
    public partial class FavouritesPanel : UserControl
    {
        private Grid grid;

        public FavouritesPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            startMethods();
        }

        protected void startMethods()
        {

        }
    }
}
