using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
   public sealed class SalesPerson:Salary
    {
        private decimal salesCom;
        public decimal SalesCom
        { get { return salesCom; } 
            set { 
                salesCom = value; } 
        }
        //Constructor
        public SalesPerson(string sin) : base(sin)
        {
            this.Sin = sin;
        }
        public SalesPerson(string sin,string first , string last) : base(sin, first, last)
        {
            this.Sin = sin;
            this.First = first;
            this.Last = last;
        }
        public SalesPerson(string sin,string first , string last,decimal salesCom ,decimal comSalary):base(sin , first ,last ) 
        {
            this.salesCom = Math.Max(0,salesCom);
            this.Amount=comSalary;
        }
        public override string ToString()
        {
            return $"{base.ToString()}" +
                    $"Your commision bonus:{Bonus():C}\n" +
                    $"Total Bonuses from Commisions {CalculatePay():C}\n";
        }
        //10% commision
        public override decimal Bonus()
        {
            return salesCom * 0.10m;
        }
        // adding Salary base pay + commision bonus
        public override decimal CalculatePay()
        {
            decimal basePay = base.CalculatePay();
            return basePay + Bonus();
        }
    }
}
