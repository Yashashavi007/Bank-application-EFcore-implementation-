using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static EFCore.Bankapp.Enums.TransactionEnum;

namespace EFCore.Bankapp.Model
{
    public class Transaction
    {
        [MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public float Amount { get; set; }

        // Navigation Property for Customer

        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

    }
}
