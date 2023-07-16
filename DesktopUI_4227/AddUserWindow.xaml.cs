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

namespace DesktopUI_4227
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow(AddUserViewModel s)
        {
            DataContext=new AddUserViewModel();
            InitializeComponent();
            DataContext=s;
            s.CloseAction = () => Close();

        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
           Window.GetWindow(this).Close();
           
        }

    }
}
