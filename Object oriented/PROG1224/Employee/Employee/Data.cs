using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Employees.Employee;

namespace Employees
{
    public static class Data
    {    
        public static List<Employee> GetEmployees()
        {  
            List<Employee> employees = new List<Employee>();
            //employee samples
            employees.Add(new Hourly(17M, 40, "123456789", "John", "Doe",true));
            employees.Add(new Hourly(17M, 43, "101112134", "Jane", "Doe", true));
            employees.Add(new Hourly(17M, 44, "101112135", "Jon", "Doe", true));
            employees.Add(new Hourly(17M, 45, "101112136", "Jen", "Doe",false));
            employees.Add(new Hourly(17M, 46, "101112137", "Jone", "Doe",false));
            //Salary samples
            employees.Add(new Salary("101112138", "John", "Stewart", 40000m, new DateTime(2010,02,01)));
            employees.Add(new Salary("101112139", "Diana", "Prince", 300000m, new DateTime(2013, 02,01)));
            employees.Add(new Salary("101112140", "Bruce", "Wayne", 500000m, new DateTime(2014,02,01)));
            employees.Add(new Salary("101112140", "Damian", "Wayne",100000m, new DateTime(2015,02,01)));
            employees.Add(new Salary("101112142", "Tony", "Stark", 300000m, new DateTime(2016,02,01)));
            //sales Person samples
            employees.Add(new SalesPerson("101112143", "Barry", "Allen", 100m,40000m));
            employees.Add(new SalesPerson("101112144", "Oliver", "Queen", 200m, 400000m));
            employees.Add(new SalesPerson("101112145", "Natasha", "Romanov",300m,45000m));
            employees.Add(new SalesPerson("101112146", "Steve", "Rogers", 400M, 50000m));
            employees.Add(new SalesPerson("101112147", "Bruce", "Banner", 500M, 60000m));




            return employees;
        }
    }
}
