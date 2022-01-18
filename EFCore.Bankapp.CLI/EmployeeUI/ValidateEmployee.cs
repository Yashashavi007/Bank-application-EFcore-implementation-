using EFCore.Bankapp.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.CLI.EmployeeUI
{
    internal class ValidateEmployee
    {
        public static void AuthenticateEmployee()
        {
            BankService Emp = new BankService();

            try
            {
                Console.WriteLine("\t\t ****** Welcome To Bank ******\n\n");

                Console.Write("\n\t\t Enter Bank name : ");
                string bank = Console.ReadLine();

                Console.Write("\n\t\t Enter your employee Id : ");
                string empID = Console.ReadLine();

                Console.Write("\n\t\t Enter your pin : ");
                int pin = Convert.ToInt32(Console.ReadLine());

                //Emp.ValidateEmployee(bank, empID, pin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
    }
}
