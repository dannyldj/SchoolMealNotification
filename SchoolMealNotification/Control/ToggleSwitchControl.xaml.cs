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

namespace SchoolMealNotification.Control
{
    /// <summary>
    /// Interaction logic for ToggleSwitchControl.xaml
    /// </summary>
    public partial class ToggleSwitchControl : UserControl
    {
        Thickness LeftSide = new Thickness(-57, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -57, 0);
        SolidColorBrush Off = new SolidColorBrush(Colors.White);
        SolidColorBrush On = new SolidColorBrush(Colors.Gray);

        private bool _toggled;
        public bool Toggled
        {
            get => _toggled;
            set
            {
                _toggled = value;
                SetToggledSwitch();
            }
        }

        public ToggleSwitchControl()
        {
            InitializeComponent();
            Toggled = false;
        }

        private void SetToggledSwitch()
        {
            if (_toggled)
            {
                bdBack.Background = On;
                bdBack.BorderThickness = new Thickness(0);
                rtDot.Margin = RightSide;
                rtDot.Fill = new SolidColorBrush(Colors.White);
                tbStatus.Text = "자세히";
            }
            else
            {
                bdBack.Background = Off;
                bdBack.BorderThickness = new Thickness(5);
                rtDot.Margin = LeftSide;
                rtDot.Fill = new SolidColorBrush(Colors.Black);
                tbStatus.Text = "간략히";
            }
        }

        private void bdBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Toggled = !Toggled;
        }

        private void rtDot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Toggled = !Toggled;
        }
    }
}
