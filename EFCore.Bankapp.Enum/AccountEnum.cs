using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.Enums
{
    public class AccountEnum
    {
        public enum AccountStatus
        {
            Active = 1,
            Closed
        }

        public enum EmployeeOperation
        {
            Update = 1,
            DeleteAccount,
            RevertTransaction,
            ViewTransactionHistory
        }
    }
}
