using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Salary:Employee
    {
        private decimal amount;
   

        public decimal Amount
        {
            get =>amount;
            set { amount = value; }
        }

        public Salary(string sin) : base(sin)
        {
            this.Sin = sin;
        }
        public Salary(string sin, string first, string last):base(sin,first ,last) 
        {
            this.Sin = sin;
            this.First = first;
            this.Last = last;
        }
        public Salary(string sin, string first, string last ,decimal amount,DateTime startDate) : base(sin, first, last)
        {
            this.Amount = amount;
            this.HireDate = startDate;
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                    $"Yearly Salary:{Amount:C2}\n" +
                    $"Bonus: {Bonus():C}\n"+ $"Total Pay:{CalculatePay():C2}\n";
        }
        public override decimal Bonus()
        {
            int years = DateTime.Now.Year - HireDate.Year;
            decimal bonus = years * 100;
            return bonus;
        }

        public override decimal CalculatePay()
        {   decimal bonus = Bonus();
            decimal total = Amount + bonus;
            decimal pay = total / 26;
            return pay;
        }
      
    }
}
