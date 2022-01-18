using EFCore.Bankapp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static EFCore.Bankapp.Enums.TransactionEnum;

namespace EFCore.Bankapp.Service
{
    public class CustomerService
    {

        public void Deposit(string accountId, string currency, float amount)
        {
            using (var context = new BankDBContext())
            {
                var account = context.Customers.Single(c => c.ID == accountId);
                var conversionRate = GetConversionRate(currency);
                account.Balance += amount * conversionRate;
                BankService.UpdateTransactionHistory(account.ID, account.ID, TransactionType.Deposit, amount * conversionRate);
                context.SaveChanges();
            }
                
            Console.WriteLine("Deposit done!!!");
        }

        public void Withdraw(string accountId, float amount)
        {
            using (var context = new BankDBContext())
            {
                var account = context.Customers.Single(c => c.ID == accountId);
                account.Balance -= amount;
                BankService.UpdateTransactionHistory(account.ID, account.ID, TransactionType.Withdraw, amount);
                context.SaveChanges();
            }
            Console.WriteLine("Withdraw done!!!");
        }

        public void Transfer(string accountId, string toAccountNumber, float amount)
        {
            using (var context = new BankDBContext())
            {
                var senderaccount = context.Customers.Single(c => c.ID == accountId);
                var receiverAccount = context.Customers.SingleOrDefault(c => c.AccountNumber == toAccountNumber);
                var transferCharge = CalculateTransferCharge(senderaccount.BankIFSCCode, receiverAccount.BankIFSCCode, amount);
                Console.WriteLine("Transfer charge"+transferCharge);

                senderaccount.Balance -= amount + amount * (transferCharge/100);
                receiverAccount.Balance += amount;
                BankService.UpdateTransactionHistory(senderaccount.ID, receiverAccount.ID, TransactionType.Transfer, amount);
                BankService.UpdateTransactionHistory(receiverAccount.ID, senderaccount.ID, TransactionType.Transfer, amount);
                context.SaveChanges();
            }
            Console.WriteLine("Transfer done!!!");
        }

        public float CalculateTransferCharge(string senderBank, string receiverBank, float amount)
        {
            if (senderBank.Substring(0,3) == receiverBank.Substring(0,3))
            {
                if (amount <= 100000)
                {
                    return BankService.IntraBankIMPS;
                }
                else
                {
                    return BankService.IntraBankRTGS;
                }
            }
            else
            {
                if (amount <= 100000)
                {
                    return BankService.InterBankIMPS;
                }
                else
                {
                    return BankService.InterBankRTGS;
                }
            }
        }

        public float GetConversionRate(string currencyName)
        {
            using (var context = new BankDBContext())
            {
                TextInfo text = new CultureInfo("en-US", false).TextInfo;
                return context.Currencies.SingleOrDefault(c => c.Name == text.ToTitleCase(currencyName)).ConversionRate;
            }
                
        }

        public float GetBalance(string accountId)
        {
            using (var context = new BankDBContext())
            {
                var account = context.Customers.Single(c => c.ID == accountId);
                return account.Balance;
            }
                
        }

        public void ViewTransactionHistory(string id)
        {
            using (var context = new BankDBContext())
            {
                List<Transaction> passbook = context.Transactions.Where(t => t.CustomerID == id).ToList();
                Console.WriteLine(passbook);
                foreach (var entry in passbook)
                {
                    Console.WriteLine($"{entry.Id} | {entry.SenderAccountNumber} | {entry.ReceiverAccountNumber} | {entry.Type} | {entry.Amount} | {entry.TimeStamp}");
                }
            }
                
        }
    }
}
