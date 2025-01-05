using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;
using static Employees.Employee;

namespace Employees
{
    public abstract class Employee: ICalcDeductions
    {   //struct
        public struct Address
        {
            public string Street;
            public string City;
            public string Province;
            public string PostalCode;
            public Address(string street, string city, string province, string postalCode)
            {
                if (string.IsNullOrEmpty(street))
                {
                    MessageDialog msg = new MessageDialog("Street cannot be Empty");
                    msg.ShowAsync();
                }
                if (string.IsNullOrEmpty(city))
                {
                    MessageDialog msg = new MessageDialog("City cannot be Empty");
                    msg.ShowAsync();
                }
                if (string.IsNullOrEmpty(province))
                {
                    MessageDialog msg = new MessageDialog("Province cannot be Empty");
                    msg.ShowAsync();
                }
                if (string.IsNullOrEmpty(postalCode))
                {
                    MessageDialog msg = new MessageDialog("Postal Code cannot be Empty");
                    msg.ShowAsync();
                }
                if
                (IsValidAddress(postalCode))
                {
                    MessageDialog msg = new MessageDialog("The address must be in the right format");
                    msg.ShowAsync();
                }   
                Street = street;
                City = city;
                Province = province;
                PostalCode = postalCode;
            }
            public override string ToString()
                  => $"Street: {Street}\n" +
                     $"City : {City}\n" +
                     $"Province:{Province}\n" +
                     $"Postal Code: {PostalCode}";
            
        }
      //instances
        private string sin;
        private string first;
        private string last;
        private DateTime hireDate;
        private DateTime birthday;
        private string email;
        private string phone;
        private Address address;
        private bool status;
        public const string Company = "Bank Of Chaldea";
        public string Sin
        {
            get => this.sin;
            set
            {
                Convert.ToInt32(value);
                if (!string.IsNullOrEmpty(value)&&value.Length == 9)
                {
                    this.sin = value;
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Sin number must be 9 digits");
                    msg.ShowAsync();
                }
            }
        }
        public string First
        {
            get => this.first;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.first = value;
                }
                else
                {
                    MessageDialog msg = new MessageDialog("First name cannot be empty");
                    msg.ShowAsync();
                }
            }
        }
        public string Last
        {
            get => this.last;
            set
            {
                if (!string.IsNullOrEmpty(value))
                { this.last = value; }
                else
                {
                    MessageDialog msg = new MessageDialog("Last name cannot be empty");
                    msg.ShowAsync();
                }
            }
        }
        public DateTime HireDate
        {
            get => this.hireDate;
            set
            {
                if (value < DateTime.Now)
                {
                    this.hireDate = value;
                }
                else
                {
                    MessageDialog msg = new MessageDialog("The hire date cant be in the Future");
                    msg.ShowAsync();
                }
            }
        }
      
        public string Birthday
        {   
            get => this.birthday.ToLongDateString();
            set
            {
                if (DateTime.TryParse(value,out DateTime parsedDate)&& parsedDate > DateTime.Now) 
                { this.birthday = parsedDate; }
                else
                {
                    MessageDialog msg = new MessageDialog("Birthday cant be in future");
                    msg.ShowAsync();
                }
            }
        }
        public Address empAddress
        {
            get => address; 

                set=>this.address = value;
        }
        public string Email 
             {
            get => this.email;
                set =>this.email = value;
                }
        public string Phone
        {
            get => this.phone;
            set
            {
                if (isValidPhone(value))
                {
                    this.phone = value;
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Please input right format 000 000 0000");
                    msg.ShowAsync();
                }
            }
        }
        public bool Status {  get => status; set => status = value;}
        //valid phone number and valid postal code
        private static bool isValidPhone(string phone)
        {
            string phoneNum = @"^\d{3} \d{3} \d{4}$";
            return !string.IsNullOrEmpty(phoneNum) && phoneNum.Length > 11 && Regex.IsMatch(phone, phoneNum);
        }
        private static bool IsValidAddress(string PostalCode)
        {
            string postCodePat = @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$";
            return Regex.IsMatch(PostalCode, postCodePat);
        }
        //Contructors
        public Employee (string sin)
        {
            this.sin = sin; 

        }
        public Employee(string sin,string first,string last)
        {
            this.sin=sin;
            this.first=first;
            this.last=last;
         
           
        }
        public virtual string ToString()
        {
            return $"Employee: {First} {Last}\n" +
                       $"Phone Number : {Phone}\n"+
                       $"{empAddress}";
        }

        public virtual decimal Bonus()
        {
            return 0M;
        }
        //abstract class Calculate Pay
        public abstract decimal CalculatePay();
        public virtual decimal IncomeTax(decimal income)
        {
            if (income <= 49000)
            {
                return income * 0.15m;
            }

            else if (income <= 98000)
            {
                return income * 0.20m;
            }

            else if (income <= 151000)
            {
                return income * 0.26m;
            }

            else if (income <= 215000)
            {
                return income * 0.29m;
            }

            else
                return income * 0.33m;
        }
        public virtual decimal Pension(decimal income)
        { return income * 0.10m; }
        public virtual decimal UnionDues(decimal income)
        {
            return 10m;
        }
        public virtual decimal Insurance(decimal income)
        {
            return 160m;
        }
    }
}

