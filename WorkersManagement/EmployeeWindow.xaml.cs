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
    public partial class EmployeeWindow : Window
    {
        private readonly AppDbContext _context = new();

        public EmployeeWindow()
        {
            InitializeComponent();
            _context.Database.EnsureCreated();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeeDataGrid.ItemsSource = null;
            EmployeeDataGrid.ItemsSource = _context.Employees.AsNoTracking().ToList();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(EmpIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmpNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmpAddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmpPhoneTextBox.Text))
                return false;

            if (EmpGenderComboBox.SelectedItem == null ||
                EmpPositionComboBox.SelectedItem == null ||
                EmpEducationComboBox.SelectedItem == null)
                return false;

            if (!EmpDOBPicker.SelectedDate.HasValue)
                return false;

            return true;
        }

        private void ClearForm()
        {
            EmpIdTextBox.Text = string.Empty;
            EmpNameTextBox.Text = string.Empty;
            EmpAddressTextBox.Text = string.Empty;
            EmpGenderComboBox.SelectedIndex = -1;
            EmpPositionComboBox.SelectedIndex = -1;
            EmpDOBPicker.SelectedDate = null;
            EmpPhoneTextBox.Text = string.Empty;
            EmpEducationComboBox.SelectedIndex = -1;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid())
            {
                MessageBox.Show("Please fill out all fields before adding an employee.", "Missing Data", MessageBoxButton.OK, MessageBoxImage.Warning); ;
                return;
            }
            if (_context.Employees.Any(e => e.EmployeeId == EmpIdTextBox.Text))
            {
                MessageBox.Show("An employee with this ID already exists.", "Duplicate ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var emp = new Employee
            {
                EmployeeId = EmpIdTextBox.Text,
                Name = EmpNameTextBox.Text,
                Address = EmpAddressTextBox.Text,
                Gender = ((ComboBoxItem)EmpGenderComboBox.SelectedItem)?.Content.ToString(),
                Position = ((ComboBoxItem)EmpPositionComboBox.SelectedItem)?.Content.ToString(),
                DOB = EmpDOBPicker.SelectedDate ?? DateTime.MinValue,
                Phone = EmpPhoneTextBox.Text,
                Education = ((ComboBoxItem)EmpEducationComboBox.SelectedItem)?.Content.ToString()
            };

            _context.Employees.Add(emp);
            _context.SaveChanges();
            LoadEmployees();
            MessageBox.Show("Employee added successfully!");
            ClearForm();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selected)
            {
                var emp = _context.Employees.FirstOrDefault(x => x.Id == selected.Id);
                if (emp == null) return;

                if (_context.Employees.Any(e => e.EmployeeId == EmpIdTextBox.Text && e.Id != emp.Id))
                {
                    MessageBox.Show("An employee with this ID already exists.", "Duplicate ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmpIdTextBox.Text = string.Empty;
                    return;
                }

                if (EmpPhoneTextBox.Text.Length != 9)
                {
                    MessageBox.Show("Phone number must be exactly 9 digits.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                emp.EmployeeId = EmpIdTextBox.Text;
                emp.Name = EmpNameTextBox.Text;
                emp.Address = EmpAddressTextBox.Text;
                emp.Gender = ((ComboBoxItem)EmpGenderComboBox.SelectedItem)?.Content.ToString();
                emp.Position = ((ComboBoxItem)EmpPositionComboBox.SelectedItem)?.Content.ToString();
                emp.DOB = EmpDOBPicker.SelectedDate ?? DateTime.MinValue;
                emp.Phone = EmpPhoneTextBox.Text;
                emp.Education = ((ComboBoxItem)EmpEducationComboBox.SelectedItem)?.Content.ToString();

                _context.SaveChanges();
                LoadEmployees();
                MessageBox.Show("Employee updated.");
                ClearForm();
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selected)
            {
                var emp = _context.Employees.FirstOrDefault(e => e.Id == selected.Id);
                if (emp != null)
                {
                    _context.Employees.Remove(emp);
                    _context.SaveChanges();
                    LoadEmployees();
                    MessageBox.Show("Employee deleted.");
                    ClearForm();
                }
            }
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selected)
            {
                EmpIdTextBox.Text = selected.EmployeeId;
                EmpNameTextBox.Text = selected.Name;
                EmpAddressTextBox.Text = selected.Address;
                EmpGenderComboBox.SelectedItem = EmpGenderComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == selected.Gender);
                EmpPositionComboBox.SelectedItem = EmpPositionComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == selected.Position);
                EmpDOBPicker.SelectedDate = selected.DOB;
                EmpPhoneTextBox.Text = selected.Phone;
                EmpEducationComboBox.SelectedItem = EmpEducationComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == selected.Education);
            }
        }
    }
}
