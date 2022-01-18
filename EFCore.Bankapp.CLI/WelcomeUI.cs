using EFCore.Bankapp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;
using EFCore.Bankapp.CLI.CustomerUI;
using EFCore.Bankapp.CLI.EmployeeUI;

namespace EFCore.Bankapp.CLI
{
    public class WelcomeUI
    {
        public static void Main()
        {
            try
            {
                using (var context = new BankDBContext())
                {
                    //context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                Console.WriteLine("\t\t Welcome to Banking Application!!");
                char ch = 'y';

                do
                {
                    Console.WriteLine("\n\t\t 1. Customer");
                    Console.WriteLine("\t\t 2. Employee");
                    Console.WriteLine("\t\t 3. Exit");
                    Console.Write("\n\t\t Select to continue : ");
                    UserType choice = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());

                    switch (choice)
                    {
                        case UserType.Customer:
                            WelcomeCustomerUI.Customer();
                            break;
                        case UserType.Employee:
                            WelcomeEmployeeUI.Employee();
                            break;
                        case UserType.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\t\t Invalid Choice!!");
                            break;
                    }

                    Console.Write("\n\t\t Do you want to perform anything else ?.. ");
                    ch = Console.ReadLine()[0];
                } while (ch == 'y' || ch == 'Y');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("\n\t\t THANK YOU! VISIT AGAIN!!!");
            }


        }
    }
}
