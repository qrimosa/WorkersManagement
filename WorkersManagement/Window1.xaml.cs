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

namespace WorkersManagement
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Open login window (MainWindow), close current window
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Show();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
        }

        private void SalaryButton_Click(object sender, RoutedEventArgs e)
        {
            SalaryWindow salaryWindow = new SalaryWindow();
            salaryWindow.Show();
        }
    }
}
