using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace De.HsFlensburg.ClientApp051.Ui.Desktop
{
    /// <summary>
    /// Interaction logic for NewClientWindow.xaml
    /// </summary>
    public partial class NewClientWindow : Window
    {
        public NewClientWindow()
        {
            InitializeComponent();
        }

        private void OpenClientwindow(
            object sender,
            RoutedEventArgs e)
        {
            NewClientWindow myWindow = new NewClientWindow();
            myWindow.ShowDialog();
        }
    }
}
