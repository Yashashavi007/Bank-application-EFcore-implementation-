using EFCore.Bankapp.Model;
using EFCore.Bankapp.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.CLI.CustomerUI
{
    internal class ValidateCustomer
    {
        private static BankService Manager = new BankService();

        public static string AuthenticateUser()
        {
            try
            {
                Customer account = null;
                Console.Write("\n\t\t Enter Bank Name : ");
                var bankName = Console.ReadLine();
                Console.Write("\n\t\t Enter Bank IFSC Code : ");
                var IfscCode = Console.ReadLine();
                var bankCheck = Manager.ValidateBank(bankName, IfscCode);

                if (bankCheck == true)
                {
                    Console.Write("\n\t\t Enter Account Number : ");
                    var accountNumber = Console.ReadLine();
                    Manager.ValidateAccount(accountNumber);

                    int check = 3;
                    do
                    {
                        check--;
                        Console.Write("\n\t\t Enter Pin : ");
                        var accPin = Convert.ToInt32(Console.ReadLine());
                        if (Manager.ValidatePin(accountNumber, accPin) == true)
                        {
                            account = Manager.GetAccount(accountNumber);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\t\t Wrond PIN!!!");
                            Console.WriteLine($"\t\t Try Again, {check} Attempts left!!!");
                        }
                    } while (check != 0);
                    return account.ID;
                }
                else
                {
                    Console.WriteLine("\t\t Bank doesn't Exist!!!!1");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
