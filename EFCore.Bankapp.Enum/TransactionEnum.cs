using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.Enums
{
    public class TransactionEnum
    {
        public enum TransactionType
        {
            Deposit = 1,
            Withdraw,
            Transfer
        }

        public enum OperationType
        {
            Deposit = 1,
            Withdraw,
            Transfer,
            CheckBalance,
            ViewPassbook,
        }
    }
}
