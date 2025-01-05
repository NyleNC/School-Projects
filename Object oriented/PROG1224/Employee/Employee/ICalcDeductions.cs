using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public interface ICalcDeductions
    {
        decimal IncomeTax(decimal income);
        decimal Pension(decimal income);
        decimal UnionDues(decimal income);
        decimal Insurance(decimal income);

    }
   
}
