using EFCore.Bankapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EFCore.Bankapp.Enums.AccountEnum;
using static EFCore.Bankapp.Enums.TransactionEnum;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.Service
{
    public class BankService
    {
        // Bank Transfer Rates
        private static float intraBankRTGS = 0;
        private static float intraBankIMPS = 5;
        private static float interBankRTGS = 2;
        private static float interBankIMPS = 6;
        public static float IntraBankRTGS
        {
            get { return intraBankRTGS; }
            set { intraBankRTGS = value; }
        }
        public static float IntraBankIMPS
        {
            get { return intraBankIMPS; }
            set { intraBankIMPS = value; }
        }
        public static float InterBankRTGS
        {
            get { return interBankRTGS; }
            set { interBankRTGS = value; }
        }
        public static float InterBankIMPS
        {
            get { return interBankIMPS; }
            set { interBankIMPS = value; }
        }

        // GENERATION
        private static string GenerateBankID(string bankName)
        {
            return (bankName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private static string GenerateAccountID(string accountHolderName)
        {
            return (accountHolderName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy"));
        }

        private static string GenerateEmployeeID(String employeeName)
        {
            var random = new Random();
            return ("EMP" + employeeName.Substring(0, 4) + random.Next(1000, 9999).ToString()) ;
        }
        private static string GenerateTransactionID(string bankId, string accountId)
        {
            return ("TXN" + bankId + accountId + DateTime.Today.ToString("dd/MM/yyyy"));
        }
        private static string GenerateAccountNumber()
        {
            var random = new Random();
            string accNo = random.Next(1, 9).ToString();

            for (int i = 0; i < 9; i++)
            {
                accNo = String.Concat(accNo, random.Next(10).ToString());
            }

            return accNo;
        }

        private static int GeneratePin()
        {
            Random random = new Random();
            return random.Next(1000, 9999);

        }

        // VALIDATION
        public bool ValidateBank(string bankName, string ifscCode)
        {
            try
            {
                using (var context = new BankDBContext())
                {
                    return context.Banks.Any(b => b.Name == bankName && b.IFSCCode == ifscCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public bool ValidateAccount(string accNo)
        {
            try
            {
                using (var context = new BankDBContext())
                {
                    return context.Customers.Any(c => c.AccountNumber == accNo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public bool ValidatePin(string accNumber, int pin)
        {
            try
            {
                using (var context = new BankDBContext())
                {
                    return context.Customers.Any(a => a.AccountNumber == accNumber && a.Pin == pin);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        // CUSTOMER OPERATION

        public static string CreateBankAccount(string bankName, string IFSCCode)
        {
            var bankId = GenerateBankID(bankName);

            using (var context = new BankDBContext())
            {
                var bank = new Bank()
                {
                    ID = bankId,
                    IFSCCode = IFSCCode,
                    Name = bankName,
                };
                context.Banks.Add(bank);

                if (context.Currencies.SingleOrDefault(c => c.Name == "INR") == null)
                {
                    AddCurrencies();
                }

                context.SaveChanges();
                return bank.ID;
            }
        }
        public static Customer CreateCustomerAccount(string bankName, string IFSCCode, string name, UserGender gender, int balance)
        {
            if (balance < 1000)
            {
                throw new Exception("Account cannot be created!!");
            }
            else
            {
                string accountNumber = GenerateAccountNumber();
                int accountPin = GeneratePin();
                string accountID = GenerateAccountID(name);
                string bankId;

                using (var context = new BankDBContext())
                {
                    var bank = context.Banks.SingleOrDefault(b => b.Name == bankName && b.IFSCCode == IFSCCode);
                    //Console.WriteLine(bankId);
                    if (bank == null)
                    {
                        bankId = CreateBankAccount(bankName, IFSCCode);
                    }
                    else
                    {
                        bankId = bank.ID;
                    }
                    var account = new Customer()
                    {
                        ID = accountID,
                        Name = name,
                        Gender = gender,
                        AccountNumber = accountNumber,
                        Balance = balance,
                        Status = AccountStatus.Active,
                        Pin = accountPin,
                        BankID = bankId,
                        BankIFSCCode = IFSCCode
                    };

                    context.Customers.Add(account);
                    context.SaveChanges();
                    return context.Customers.SingleOrDefault(c => c.ID == accountID);
                }

            }

        }

        public static void DeleteCustomerAccount(string accId)
        {
            using (var context = new BankDBContext())
            {
                try
                {
                    var account = context.Customers.SingleOrDefault(c => c.ID == accId);
                    account.Status = AccountStatus.Closed;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public Customer GetAccount(string accountNumber)
        {
            using (var context = new BankDBContext())
            {
                try
                {

                    var account = context.Customers.Single(c => c.AccountNumber == accountNumber);

                    Console.WriteLine(account.GetType());
                    return account;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return null;
        }

        public static void RetrieveData()
        {
            using (var context = new BankDBContext())
            {
                if (context.Customers.Count() != 0)
                {
                    foreach (var entry in context.Customers)
                    {
                        Console.WriteLine($"{entry.Name} | {entry.AccountNumber} | {entry.Bank} | {entry.Gender} | {entry.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("Customer table is Empty!!");
                }
            }
        }

        public static void UpdateTransactionHistory(string fromId, string toId, TransactionType type, float amount)
        {
            using (var context = new BankDBContext())
            {
                try
                {
                    var sender = context.Customers.SingleOrDefault(s => s.ID == fromId);
                    //Console.WriteLine(sender.Name);
                    var receiver = context.Customers.SingleOrDefault(r => r.ID == toId);
                    //Console.WriteLine(receiver.Name);
                    string transactionId = GenerateTransactionID(receiver.BankID, receiver.ID);

                    var transaction = new Transaction()
                    {
                        Id = transactionId,
                        SenderAccountNumber = sender.AccountNumber,
                        ReceiverAccountNumber = receiver.AccountNumber,
                        Type = type,
                        TimeStamp = DateTime.Now,
                        Amount = amount,
                        CustomerID = toId
                    };

                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                    Console.WriteLine("Transaction Updated!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static void AddCurrencies()
        {
            using (var context = new BankDBContext())
            {
                List<Currency> Data = new List<Currency>();

                Data.Add(new Currency()
                {
                    Name = "INR",
                    ConversionRate = 1
                });

                Data.Add(new Currency()
                {
                    Name = "Dollar",
                    ConversionRate = 73.92f
                });

                Data.Add(new Currency()
                {
                    Name = "Euro",
                    ConversionRate = 83.86f
                });

                Data.Add(new Currency()
                {
                    Name = "Australian Dollar",
                    ConversionRate = 53.10f
                });

                Data.Add(new Currency()
                {
                    Name = "British Pound",
                    ConversionRate = 100.43f
                });

                context.Currencies.AddRange(Data);
                context.SaveChanges();

            }
        }


        //EMPLOYEE OPERATIONS

        public static Employee CreateEmployeeAccount(string bankName, string IFSCCode, string name, UserGender gender, EmployeeRole role)
        {
            var empId = GenerateEmployeeID(name);
            int accPin = GeneratePin();
            string bankId;
            using (var context = new BankDBContext())
            {
                var bank = context.Banks.SingleOrDefault(b => b.Name == bankName && b.IFSCCode == IFSCCode);
                if(bank == null)
                {
                    bankId = CreateBankAccount(bankName, IFSCCode);
                }
                else
                {
                    bankId = bank.ID;
                }
                var employee = new Employee()
                {
                    ID = empId,
                    Name = name,
                    Gender = gender,
                    Pin = accPin,
                    Role = role,
                    BankID = bankId,
                    BankIFSCCode = IFSCCode
                };

                context.Employees.Add(employee);
                context.SaveChanges();

                return context.Employees.SingleOrDefault(e => e.ID == empId);
            }

        }
    }
}
