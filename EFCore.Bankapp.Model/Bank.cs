using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Bankapp.Model
{
    //[Table("BankName")]  --> To give table name using data annotations
    public class Bank
    {
        [MaxLength(100)]
        public string ID { get; set; }
        [MaxLength(100)]
        public string IFSCCode { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation Property for Customers
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
