using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Hourly:Employee
    {
        private decimal rate;
        private decimal hours;
         public decimal Rate
        {get => rate;
            set { rate = value; }
        }

        public decimal Hours
        {
            get => hours;
            set { hours = value; }
        }
        //Constructors
        public Hourly(string sin) :base(sin)
        {
            this.Sin =sin;
        }
        public Hourly(string sin,string first,string last) : base(sin, first, last)
        {
            this.Sin= sin;
            this.First = first;
            this.Last = last;
          

        }
        public Hourly(decimal rate, decimal hours, string sin, string first, string last , bool isActive) : base(sin,  first, last)
        {
            this.rate = rate;
            this.hours = hours;
            this.Status = isActive;
           
        }
        public override string ToString()
        {
            return $"{base.ToString()}\n"+
                $"Hourly pay total every 2 weeks: {CalculatePay():C}\n";
        }
        public override decimal Bonus()
        {
            return 0m;

        }

        // employee hourly pay for 2 weeks
        public override decimal CalculatePay()
        {
            decimal pay = rate * hours * 2;

            return pay;
        }
    }
}
