using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees;

namespace Payroll
{
    public class PayPeriod <T> where T : Employee
    {
        private DateTime payPeriod;
        private List<T> empPayPeriod;

        public PayPeriod(DateTime payPeriod, List<T> empPayPeriod)
        {
            this.payPeriod = payPeriod;
            this.empPayPeriod = empPayPeriod;
        }


        public List<string> ProcessPayroll()
        {
            List<string> result = new List<string>();
            decimal totalPay = 0;
            decimal totalBonus = 0;
            decimal totalDeductions = 0;
            foreach (T employee in empPayPeriod)
            {
                decimal pay = employee.CalculatePay();
                decimal bonus = employee.Bonus();
                decimal deductions = employee.IncomeTax(pay) + employee.Pension(pay) + employee.UnionDues(pay)+employee.Insurance(pay);
                totalPay += pay;
                totalBonus += bonus;
                totalDeductions += deductions;
                string empInfo = $"{employee.Sin} {employee.First} {employee.Last} " +
                    $"Net: {pay - deductions:C} - Bonus: {bonus:C} - Deductions: {deductions:C}";
                result.Add(empInfo);
            }
            TotalEmployees = empPayPeriod.Count;
            TotalPay = totalPay;
            TotalBonus = totalBonus;
            TotalDeductions = totalDeductions;

            return result;
        }
        public int TotalEmployees { get; private set; }
        public decimal TotalPay { get; private set; }
        public decimal TotalBonus { get; private set; }
        public decimal TotalDeductions { get; private set; }
    }
}
