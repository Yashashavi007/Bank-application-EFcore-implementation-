using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.Model
{
    public class Employee : User
    {
        public EmployeeRole Role { get; set; }
        public int Pin { get; set; }

        //Navigation Property for Bank
        public string BankID { get; set; }
        public string BankIFSCCode { get; set; }
        public virtual Bank Bank { get; set; }

    }
}
