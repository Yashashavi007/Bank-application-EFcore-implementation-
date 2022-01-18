using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.Enums
{
    public class UserEnum
    {
        public enum UserGender
        {
            Male = 1,
            Female,
            Other
        }

        public enum UserType
        {
            Customer = 1,
            Employee,
            Exit
        }

        public enum CustomerType
        {
            NewCustomer = 1,
            ExistingCustomer,
            Exit
        }

        public enum EmployeeType
        {
            NewEmployee = 1,
            ExistingEmployee,
            Exit
        }

        public enum EmployeeRole
        {
            Manager = 1,
            Cashier,
            Helper
        }

    }
}
