using EFCore.Bankapp.Model;
using EFCore.Bankapp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.TransactionEnum;

namespace EFCore.Bankapp.CLI.CustomerUI
{
    internal class ExistingCustomerUI
    {
        public void ExistingCustomer()
        {
            try
            {
                BankService Manager = new BankService();
                CustomerService Service = new CustomerService();
                string accountId = ValidateCustomer.AuthenticateUser();
                Console.WriteLine(accountId);
                if (accountId != null)
                {
                    char ch = 'y';
                    do
                    {
                        Console.WriteLine("\n\t\t <-- Operation List -->");
                        Console.WriteLine("\t\t 1. Deposit Amount");
                        Console.WriteLine("\t\t 2. Withdraw Amount");
                        Console.WriteLine("\t\t 3. Transfer Amount");
                        Console.WriteLine("\t\t 4. Check Balance");
                        Console.WriteLine("\t\t 5. Print e-Passbook");

                        Console.Write("\t\t What you would like to do today? ");
                        //int choice = Convert.ToInt32(Console.ReadLine());
                        OperationType userChoice = (OperationType)Enum.Parse(typeof(OperationType), Console.ReadLine());

                        switch (userChoice)
                        {
                            case OperationType.Deposit:
                                try
                                {
                                    Console.Write("\t\t Enter Currency : ");
                                    string currency = Console.ReadLine();
                                    Console.Write("\t\t Enter Amount : ");
                                    int dAmount = Convert.ToInt32(Console.ReadLine());
                                    Service.Deposit(accountId, currency, dAmount);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case OperationType.Withdraw:
                                try
                                {
                                    Console.Write("\t\t Enter Amount : ");
                                    int wAmount = Convert.ToInt32(Console.ReadLine());
                                    Service.Withdraw(accountId, wAmount);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case OperationType.Transfer:
                                Console.Write("\n\t\t Enter Receiver's Bank Name : ");
                                string rBankName = Console.ReadLine();
                                Console.Write("\n\t\t Enter Receiver's Account number : ");
                                string rAccountNumber = Console.ReadLine();
                                if (Manager.ValidateAccount(rAccountNumber) == true)
                                {
                                    Console.Write("\t\t Enter Amount : ");
                                    int tAmount = Convert.ToInt32(Console.ReadLine());
                                    Service.Transfer(accountId, rAccountNumber, tAmount);
                                    Console.WriteLine("\t\t Transfer Successfull!!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Receiver's Account!!!");
                                }
                                break;
                            case OperationType.CheckBalance:
                                Console.WriteLine($"\t\t Current balance : {Service.GetBalance(accountId)} Rupees");
                                //Console.WriteLine($"\t\t Current balance : {account.Balance} Rupees");
                                break;
                            case OperationType.ViewPassbook:
                                Console.WriteLine("----- TRANSACTION HISTORY -----");
                                Service.ViewTransactionHistory(accountId);
                                break;
                            default:
                                Console.WriteLine("\t\t Invalid Choice!!");
                                break;
                        }

                        Console.Write("\n\t\t Would you like to perform more operations? (Y/N) : ");
                        ch = Console.ReadLine()[0];
                    } while (ch == 'y' || ch == 'Y');

                }
                else
                {
                    Console.WriteLine("/t/t Account doesn't exist!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
