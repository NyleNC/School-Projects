using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Employees;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Employees.Employee;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Payroll
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            TestEmployeeClasses();
            CalculateStatistics();

        }
        private void TestEmployeeClasses()
        {
            var employees = Data.GetEmployees();
            empList.ItemsSource = employees;
        }
        List<Employee> employees = new List<Employee>();
        private void employeeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedType = (empType.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedType == "All Employees")
            {
                empList.ItemsSource = Data.GetEmployees();
            }
            else
            {
                var filteredEmployees = Data.GetEmployees().Where(emp => emp.GetType().Name == selectedType).ToList();
                empList.ItemsSource = filteredEmployees;
            }
            switch (selectedType)
            {
                case "Hourly":
                    hourlyInputs.Visibility = Visibility.Visible;
                    salaryInputs.Visibility = Visibility.Collapsed;
                    salesInputs.Visibility = Visibility.Collapsed;
                    txtSala.Visibility = Visibility.Collapsed;
                    txtHoursWorked.Visibility = Visibility.Visible;
                    txtRate.Visibility = Visibility.Visible;
                    txtCom.Visibility = Visibility.Collapsed;

                    break;
                case "Salary":
                    salaryInputs.Visibility = Visibility.Visible;
                    hourlyInputs.Visibility= Visibility.Collapsed;
                    salesInputs.Visibility= Visibility.Collapsed;
                    txtHoursWorked.Visibility = Visibility.Collapsed;
                    txtRate.Visibility = Visibility.Collapsed;
                    txtCom.Visibility = Visibility.Collapsed;
                    txtSala.Visibility = Visibility.Visible;
                    break;
                case "SalesPerson":
                    salesInputs.Visibility = Visibility.Visible;
                    salaryInputs.Visibility= Visibility.Collapsed;
                    hourlyInputs.Visibility= Visibility.Collapsed;
                    txtHoursWorked.Visibility = Visibility.Collapsed;
                    txtRate.Visibility = Visibility.Collapsed;
                    txtCom.Visibility = Visibility.Visible;
                    txtSal.Visibility = Visibility.Visible;
                    break;
                default:
                 break;
            }
        }
        //if the user inputed the right employee information and it matches
        private void NameKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                try
                {
                    string input = txtEmp.Text.Trim();
                    if (string.IsNullOrEmpty(input))
                    {
                        MessageDialog msg = new MessageDialog("enter sin number or last to search");
                        msg.ShowAsync();
                    }
                    else if  (input.Length == 9 && input.All(char.IsDigit))
                    {
                        var filteredEmployees = Data.GetEmployees()
                   .Where(employee => employee.Sin == input)
                   .ToList();
                        empList.ItemsSource = filteredEmployees;
                        if (filteredEmployees.Count == 0)
                        {
                            MessageDialog msg=new MessageDialog($"No employee with the sin number {input}");
                            msg.ShowAsync();
                        }
                    }
                    else
                    {
                       var filteredEmployees = Data.GetEmployees()
                      .Where(employee => employee.Last.ToLower().Equals(input.ToLower()))
                      .ToList();

                        empList.ItemsSource = filteredEmployees;
                        if(filteredEmployees.Count == 0)
                        {
                            MessageDialog msg = new MessageDialog($"There are no matching employee with that last name {input}");
                            msg.ShowAsync();
                        }
                    }
                    txtEmp.Text = "";
                }
                catch (Exception)
                {
                   MessageDialog msg=new MessageDialog("Something went wrong");
                    msg.ShowAsync();
                }

            }
        }
   
        private void chkAdd_Checked(object sender, RoutedEventArgs e)
        {
            AddDetails.Visibility = Visibility.Visible;
            subDetails.Visibility = Visibility.Collapsed;
            empList.Visibility = Visibility.Collapsed;
            btnAdd.Visibility = Visibility.Visible;
            txtPayRate.Visibility = Visibility.Collapsed;
            empDate.Visibility = Visibility.Visible;
        }

        private void chkAdd_Unchecked(object sender, RoutedEventArgs e)
        {
            AddDetails.Visibility = Visibility.Collapsed;
            subDetails.Visibility = Visibility.Visible;
            empList.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Collapsed;
            txtPayRate.Visibility = Visibility.Visible;
            empDate.Visibility= Visibility.Collapsed;
        }

        private void bntAdd_Click(object sender, RoutedEventArgs e)
        {
            string selectedType = (empType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string sin = txtSin.Text;
            string first = txtName.Text;
            string last = txtLast.Text;
            bool isActive=true;
            decimal rate = 0;
            decimal hours = 0; 
            decimal salary = 0;
            decimal commission = 0;
            DateTime startDate = empDate.Date.DateTime;
            //validation 
            if (string.IsNullOrEmpty(sin))
            {
                ShowMessage("It needs Sin Number");
            }
            else if ((sin.Length == 9 && sin.All(char.IsDigit)))
            {
                var filteredEmployees = Data.GetEmployees()
             .Where(employee => employee.Sin == sin)
             .ToList();
                if (filteredEmployees.Any())
                {
                    ShowMessage("A matching employee with the provided SIN number already exists.");
                }

                if (string.IsNullOrEmpty(first))
                {
                    ShowMessage("Please input a first Name ");
                    return;
                }
                if (string.IsNullOrEmpty(last))
                {
                    ShowMessage("Please in put a Last name");
                    return;
                }

               
                if (startDate > DateTime.Now)
                {
                    ShowMessage("start date cannot be from the future");
                    return;
                }
                else if (startDate == DateTime.MinValue) 
                {
                    ShowMessage("Please select a valid start date");
                    return;
                }
                if (hourlyInputs.Visibility == Visibility.Visible)
                {
                    if (!string.IsNullOrEmpty(txtRate.Text) && decimal.TryParse(txtRate.Text, out rate))//if its not empty
                    {

                    }
                    else
                    {
                        ShowMessage("Please enter a valid rate.");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtHoursWorked.Text) && decimal.TryParse(txtHoursWorked.Text, out hours))//if its not empty
                    {

                    }
                    else
                    {
                        ShowMessage("Please input the hours worked");
                        return;
                    }
                }

                if (salaryInputs.Visibility == Visibility.Visible)
                {
                    if (!string.IsNullOrEmpty(txtSala.Text) && decimal.TryParse(txtSala.Text, out salary))//if its not empty
                    {

                    }
                    else
                    {
                        ShowMessage("Please enter a valid salary.");
                        return;
                    }
                }

                if (salesInputs.Visibility == Visibility.Visible)
                {
                    if (!string.IsNullOrEmpty(txtCom.Text) && decimal.TryParse(txtCom.Text, out commission))//if its not empty
                    {

                    }
                    else
                    {
                        ShowMessage("Please enter a valid commission.");
                        return;
                    }
                }

                
                switch (selectedType)
                {
                    case "Hourly":
                        Employee hourlyEmployee = new Hourly(rate, hours, sin, first, last, isActive);
                        employees.Add(hourlyEmployee);
                        ShowMessage($"{first} {last}: has been Added");
                        break;
                    case "Salary":
                        Employee salEmployee = new Salary(sin, first, last, salary, startDate);
                        employees.Add(salEmployee);
                        ShowMessage($"{first} {last}: has been Added");
                        break;
                    case "SalesPerson":
                        Employee salesEmployee = new SalesPerson(sin, first, last, commission, salary);
                        employees.Add(salesEmployee);
                        ShowMessage($"{first} {last}: has been Added");
                        break;
                }
            }
        }
        private async void ShowMessage(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
  
        private void StartDate_DateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
          DateFilter(sender);
        }

        private void DateFilter(DatePicker picker)
        {
            DateTime selectedDate = picker.Date.DateTime;
            var filteredEmployees = Data.GetEmployees().Where(emp => emp.HireDate.Date == selectedDate.Date).ToList();
            empList.ItemsSource = filteredEmployees;
        }

  

        private void DisplayPayrollInformation(DateTime payPeriodDate, List<Employee> empPayPeriod)
        {
            var filteredEmployees = (List<Employee>)empList.ItemsSource;

            PayPeriod<Employee> payPeriod = new PayPeriod<Employee>(payPeriodDate, filteredEmployees);
            List<string> payrollInfo = payPeriod.ProcessPayroll();

            lblOutput.Text = string.Join("\n", payrollInfo);
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = empDate.Date.DateTime;
            var filteredEmployees = Data.GetEmployees().Where(emp => emp.HireDate.Date == selectedDate.Date).ToList();
            
            DisplayPayrollInformation(selectedDate,filteredEmployees);
            
        }

        private void EmpList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Employee selectedEmployee)
            {
                PopulateInputFields(selectedEmployee);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (empList.SelectedItem != null)
            {
                var selectedEmployee = (Employee)empList.SelectedItem;

                try
                {
                    UpdateEmployeeData(selectedEmployee);
                    ShowMessage("Employee data updated successfully.");
                }
                catch (Exception)
                {
                    ShowMessage("Invalid input format. Please enter valid numeric values.");
                }
            }
            else
            {
                ShowMessage("Please select an employee to update.");
            }
        }

        private void PopulateInputFields(Employee selectedEmployee)
        {   
            txtSin.Text = selectedEmployee.Sin;
            txtName.Text = selectedEmployee.First;
            txtLast.Text = selectedEmployee.Last;

            if (selectedEmployee is Hourly hourlyEmployee)
            {
                txtRate.Text = hourlyEmployee.Rate.ToString();
                txtHour.Text = hourlyEmployee.Hours.ToString();
            }
            else if (selectedEmployee is Salary salaryEmployee)
            {
                txtSal.Text = salaryEmployee.Amount.ToString();
            }
            else if (selectedEmployee is SalesPerson salesEmployee)
            {
                txtcomUp.Text = salesEmployee.SalesCom.ToString();
                txtSal.Text = salesEmployee.Amount.ToString();
            }
        }
        private void UpdateEmployeeData(Employee selectedEmployee)
        {
            string updateMessage = "";

            if (selectedEmployee is Hourly hourlyEmployee)
            {
                hourlyEmployee.Rate = Convert.ToDecimal(txtPayRate.Text);
                hourlyEmployee.Hours = Convert.ToDecimal(txtHour.Text);

                updateMessage = $"{hourlyEmployee.First} {hourlyEmployee.Last} updated: Rate set to {hourlyEmployee.Rate}, Hours worked set to {hourlyEmployee.Hours} ";

            }
            else if (selectedEmployee is Salary salaryEmployee)
            {
                salaryEmployee.Amount = Convert.ToDecimal(txtSal.Text);

                updateMessage = $"Salary Employee updated: Amount set to {salaryEmployee.Amount}";
            }
            else if (selectedEmployee is SalesPerson salesEmployee)
            {
                salesEmployee.SalesCom = Convert.ToDecimal(txtcomUp.Text);
                salesEmployee.Amount = Convert.ToDecimal(txtSal.Text);

                updateMessage = $"Sales Person Employee updated: Sales Commission set to {salesEmployee.SalesCom}, Amount set to {salesEmployee.Amount}";
            }
            else
            {
                ShowMessage("Please select a valid employee type.");
                return;
            }

            lblOutput.Text = updateMessage;
        }
        //delegate
        public delegate void CheckMaxPayThreshold();
        public event CheckMaxPayThreshold CheckPay;
        private Salary salaryEmployee = new Salary("", "", "", 0, DateTime.Now);
        private void MaxPayThreshold(decimal amount)
        {
            if (amount>=1000000m && CheckPay !=null)
            {
                CheckPay();
            }
            if (amount > 0m)
            {
                salaryEmployee.Amount += amount;
               
            }
        }
        private void CalculateStatistics()
        {
            var employees = Data.GetEmployees();
            // Total number of each employee type
            var employeeTypeCounts = employees.Count();

            // Total number of active and inactive employees
            var hourlyEmployees = employees.OfType<Hourly>();
            var activeEmployeesCount = hourlyEmployees.Count(emp => emp.Status);
            var inactiveEmployeesCount = hourlyEmployees.Count(emp => !emp.Status);

            // Employee with the most and least seniority
            var mostSeniorEmployee = employees
            .OrderByDescending(emp => emp.HireDate)
            .Select(emp => new { Name = emp.First + " " + emp.Last, 
                Type = emp.GetType().Name, Pay = Math.Round(emp.CalculatePay()) })
            .FirstOrDefault();
            var leastSeniorEmployee = employees
              .OrderBy(emp => emp.HireDate)
              .Select(emp => new { Name = emp.First + " " + emp.Last ,
              Type=emp.GetType().Name,Pay=Math.Round(emp.CalculatePay())})
              .FirstOrDefault();

            // Employee with the highest pay
            var highestPaidEmployee = employees
        .Select(emp => new {
            Name = emp.First + " " + emp.Last,
            Type = emp.GetType().Name,
              Pay = Math.Round(emp.CalculatePay(), 2)
          })
          .OrderByDescending(emp => emp.Pay)
          .FirstOrDefault();
            //average pay
            var average = employees.Average(emp=>emp.CalculatePay());

            //Employee with longest Name
                var employeeWithLongestName = employees
           .OrderByDescending(emp => (emp.First + " " + emp.Last).Length)
           .Select(emp => new { First = emp.First, Last = emp.Last })
           .FirstOrDefault();

            lblOutput.Text += $"Total Number of Each type: {employeeTypeCounts}\n";
            lblOutput.Text += $"Active Employees Count: {activeEmployeesCount}\n";
            lblOutput.Text += $"Inactive Employees Count: {inactiveEmployeesCount}\n";
            lblOutput.Text += $"Most Senior Employee: {mostSeniorEmployee}\n";
            lblOutput.Text += $"Least Senior Employee: {leastSeniorEmployee}\n";
            lblOutput.Text += $"Highest Paid Employee: {highestPaidEmployee}\n";
            lblOutput.Text += $"Employee With Longest Name: {employeeWithLongestName}\n";
            lblOutput.Text += $"Average pay of all employee {average:C}\n";


        }
    }

}





