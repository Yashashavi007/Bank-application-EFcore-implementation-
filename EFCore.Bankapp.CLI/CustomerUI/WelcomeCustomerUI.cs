using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.CLI.CustomerUI
{
    internal class WelcomeCustomerUI
    {
        public static void Customer()
        {
            Console.WriteLine("\t\t ****** Welcome To Bank ******\n\n");

            try
            {
                char flag = 'y';

                do
                {
                    Console.WriteLine("\t\t 1. New Customer");
                    Console.WriteLine("\t\t 2. Existing Customer");
                    Console.WriteLine("\t\t 3. Exit");
                    Console.Write("\t\t Enter your choice : ");

                    CustomerType userChoice = (CustomerType)Enum.Parse(typeof(CustomerType), Console.ReadLine());

                    //MAIN MENU SWITCH
                    switch (userChoice)
                    {
                        case CustomerType.NewCustomer:
                            NewCustomerUI obj1 = new NewCustomerUI();
                            obj1.NewCustomer();
                            break;
                        case CustomerType.ExistingCustomer:
                            ExistingCustomerUI obj2 = new ExistingCustomerUI();
                            obj2.ExistingCustomer();
                            break;
                        case CustomerType.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\t\t Invalid Choice!!!");
                            break;
                    }

                    Console.Write("\n\t\t Do you want to perform any more operation?(Y/N) ");
                    flag = Console.ReadLine()[0];
                } while (flag == 'y' || flag == 'Y');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
