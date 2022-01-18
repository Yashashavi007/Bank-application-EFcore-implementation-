using System;
using System.Collections.Generic;
using System.Text;
using static EFCore.Bankapp.Enums.AccountEnum;

namespace EFCore.Bankapp.CLI.EmployeeUI
{
    internal class ExistingEmployeeUI
    {
        public void ExistingEmployee()
        {
            try
            {
                ValidateEmployee.AuthenticateEmployee();
                char ch1 = 'y';
                do
                {
                    Console.WriteLine("\t\t ######### SERVICES #########");
                    Console.WriteLine("\t\t 1. Update Account Details");
                    Console.WriteLine("\t\t 2. Delete Account");
                    Console.WriteLine("\t\t 3. Revert Transaction");
                    Console.WriteLine("\t\t 4. View Transaction History");

                    Console.WriteLine("\n\t\t What would you like to do ?");
                    Console.Write("\n Enter your choice: ");
                    EmployeeOperation ch = (EmployeeOperation)Enum.Parse(typeof(EmployeeOperation), Console.ReadLine());
                    

                } while (true);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
