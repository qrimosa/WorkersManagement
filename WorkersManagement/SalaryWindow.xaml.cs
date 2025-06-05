using Microsoft.EntityFrameworkCore;
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
using WorkersManagement.Models;

namespace WorkersManagement
{
    public partial class SalaryWindow : Window
    {
        private readonly AppDbContext _context = new();

        private Employee? currentEmployee;
        private decimal hourlyRate;

        public SalaryWindow()
        {
            InitializeComponent();
            _context.Database.EnsureCreated();
        }

        private void FetchData_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmpIdInput.Text))
            {
                MessageBox.Show("Please enter an employee ID.");
                return;
            }

            currentEmployee = _context.Employees
                .AsNoTracking()
                .FirstOrDefault(emp => emp.EmployeeId == EmpIdInput.Text.Trim());

            if (currentEmployee == null)
            {
                MessageBox.Show("Employee not found.");
                return;
            }

            EmpNameTextBlock.Text = currentEmployee.Name;
            EmpPositionTextBlock.Text = currentEmployee.Position;

            hourlyRate = GetHourlyRate(currentEmployee.Position);
        }

        private void ViewSalary_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee == null)
            {
                MessageBox.Show("Please fetch employee data first.");
                return;
            }

            if (!decimal.TryParse(HoursWorkedInput.Text, out decimal hoursWorked) || hoursWorked < 0)
            {
                MessageBox.Show("Please enter a valid number of hours worked.");
                return;
            }

            decimal totalSalary = hoursWorked * hourlyRate;

            EmpIdOutputText.Text = currentEmployee.EmployeeId;
            EmpNameOutputText.Text = currentEmployee.Name;
            EmpPositionOutputText.Text = currentEmployee.Position;
            HoursWorkedTextBlock.Text = $"{hoursWorked}";
            DailySalaryTextBlock.Text = $"{hourlyRate:F2}"; 
            TotalAmountTextBlock.Text = $"{totalSalary:F2} zł";
        }

        private decimal GetHourlyRate(string position)
        {
            return position switch
            {
                "Manager" => 1000m / 8,
                "Developer" => 800m / 8,
                "Accountant" => 850m / 8,
                "Intern" => 400m / 8,
                _ => 600m / 8 
            };
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
