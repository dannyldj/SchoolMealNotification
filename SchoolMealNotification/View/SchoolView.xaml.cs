using SchoolMealNotification.ViewModel;
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

namespace SchoolMealNotification.View
{
    /// <summary>
    /// Interaction logic for SchoolView.xaml
    /// </summary>
    public partial class SchoolView : Window
    {
        public SchoolView()
        {
            InitializeComponent();
        }

        private void btnSelectClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lvSearchedSchool.SelectedItem.ToString()))
            {
                Close();
            }
        }
    }
}
