using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using WorkersManagement.Models;

namespace WorkersManagement
{
    public partial class ViewWindow : Window
    {
        private readonly AppDbContext _context = new();

        public ViewWindow()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string empId = SearchIdTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(empId))
            {
                MessageBox.Show("Please enter a valid Employee ID.");
                return;
            }

            var emp = _context.Employees.FirstOrDefault(x => x.EmployeeId == empId);

            if (emp != null)
            {
                IdText.Text = emp.EmployeeId;
                NameText.Text = emp.Name;
                AddressText.Text = emp.Address;
                GenderText.Text = emp.Gender;
                PositionText.Text = emp.Position;
                DOBText.Text = emp.DOB.ToString("yyyy-MM-dd");
                PhoneText.Text = emp.Phone;
                EducationText.Text = emp.Education;
            }
            else
            {
                MessageBox.Show("Employee not found.");
                ClearDisplay();
            }
        }

        private void ClearDisplay()
        {
            IdText.Text = "";
            NameText.Text = "";
            AddressText.Text = "";
            GenderText.Text = "";
            PositionText.Text = "";
            DOBText.Text = "";
            PhoneText.Text = "";
            EducationText.Text = "";
        }
    }
}
