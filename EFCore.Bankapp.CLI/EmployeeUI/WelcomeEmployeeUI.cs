using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.CLI.EmployeeUI
{
    internal class WelcomeEmployeeUI
    {
        public static void Employee()
        {
            Console.WriteLine("\t\t ****** Welcome To Bank ******\n\n");
            try
            {
                char flag = 'y';

                do
                {
                    Console.WriteLine("\t\t 1. New Employee");
                    Console.WriteLine("\t\t 2. Existing Employee");
                    Console.WriteLine("\t\t 3. Exit");

                    Console.Write("\t\t Enter your choice : ");

                    EmployeeType userChoice = (EmployeeType)Enum.Parse(typeof(EmployeeType), Console.ReadLine());

                    //MAIN MENU SWITCH
                    switch (userChoice)
                    {
                        case EmployeeType.NewEmployee:
                            NewEmployeeUI obj1 = new NewEmployeeUI();
                            obj1.NewEmployee();
                            break;
                        case EmployeeType.ExistingEmployee:
                            ExistingEmployeeUI obj2 = new ExistingEmployeeUI();
                            obj2.ExistingEmployee();
                            break;
                        case EmployeeType.Exit:
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
