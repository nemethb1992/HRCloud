using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace pmk_cv.Control
{
    class PageControl
    {
        public void searchInputPlaceholder2(TextBox tbx, Label label )
        {
            if (tbx.Text == "")
            {
                label.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

    }
}
