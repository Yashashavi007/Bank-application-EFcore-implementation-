using EFCore.Bankapp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.CLI.CustomerUI
{
    internal class NewCustomerUI
    {
        public void NewCustomer()
        {
            Console.WriteLine("\n\t\t Fill the form to create a new account.");

            Console.Write("\t\t Enter Bank Name : ");
            string bankName = Console.ReadLine().ToUpper();

            Console.Write("\t\t Enter Bank IFSC code : ");
            string IFSCcode = Console.ReadLine().ToUpper();

            Console.Write("\t\t Enter your name : ");
            string customerName = Console.ReadLine().ToUpper();

            Console.WriteLine("\n\t\t 1. Male ");
            Console.WriteLine("\t\t 2. Female ");
            Console.WriteLine("\t\t 3. Unknown ");
            Console.Write("\n\t\t Enter your Gender : ");
            UserGender Gender = (UserGender)Enum.Parse(typeof(UserGender), Console.ReadLine());

            Console.Write("\n\t\t Enter initial amount(in Rupees) : ");
            int amount = Convert.ToInt32(Console.ReadLine());


            //GOTO
            try
            {
                var newCustomer = BankService.CreateCustomerAccount(bankName, IFSCcode, customerName, Gender, amount);
                Console.WriteLine("\t\t Account created successfully!!");
                Console.WriteLine("\t\t Keep your account details for future reference!");

                Console.WriteLine($"\t\t Account Holder's name : {newCustomer.Name}");
                Console.WriteLine($"\t\t Account Holder's gender : {newCustomer.Gender}");
                Console.WriteLine($"\t\t Account number : {newCustomer.AccountNumber}");
                Console.WriteLine($"\t\t Account pin : {newCustomer.Pin}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //Call goto
            }



        }
    }
}
