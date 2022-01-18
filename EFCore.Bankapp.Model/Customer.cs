using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static EFCore.Bankapp.Enums.AccountEnum;

namespace EFCore.Bankapp.Model
{
    public class Customer : User
    {
        [MaxLength(20)]
        public string AccountNumber { get; set; }
        public int Pin { get; set; }
        public float Balance { get; set; }
        public AccountStatus Status { get; set; }

        // Navigation Property for Bank Model

        public string BankID { get; set; }
        public string BankIFSCCode { get; set; }
        public Bank Bank { get; set; }

        // Navigation Property for transactions
        public ICollection<Transaction> Transactions { get; set; }
    }
}
