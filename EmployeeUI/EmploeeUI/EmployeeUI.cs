using System;
using System.Collections.Generic;
using System.IO;
using SharedLibrary;


namespace EmployeeUI
{
    public class EmployeeUI
    {
        Employee root = new Employee("001", "password", "1", "root", "rootson", "rootmail", "rootvägen", "CEO of MEGAROOT", "All the munnies!");
        public Employee currentUser = new Employee();
        public Employee selectedUser = new Employee();
        public EmployeeManager management = new EmployeeManager();
        public bool LoggedOn = false;


        public void EmployeeLogon()
        {
            Console.Clear();
            Console.WriteLine("SETUP>>");
            List<Employee> employeeList = management.TryLoadEmployeesFromFile();

            employeeList.Add(root);

            management.FindAdminOrRoot(employeeList);

            Console.Clear();
            Console.WriteLine("Hello and welcome to the Employee UI of EmployeeManager\n");
            while (!LoggedOn)
            {
                var validEmployee = management.ValidateEmployee(employeeList);
                if (validEmployee.Admin == "0")
                {
                    currentUser = validEmployee;

                    Console.WriteLine("You successfully logged on as an Employee!");
                    LoggedOn = true;
                }
                if (validEmployee.Admin == "1")
                {
                    currentUser = validEmployee;

                    Console.WriteLine("You successfully logged on as an Employee\nYou also have access to the AdminUI!");
                    LoggedOn = true;
                }

                else
                {
                    Console.WriteLine("Login was unsuccessful...");
                }
            }
            if (LoggedOn)
            {
                EmployeeMenu(employeeList);
            }
        }

        public void EmployeeMenu(List<Employee> employeeList)
        {
            var menuChoice = Console.ReadLine();
            while (menuChoice != "quit")
            {
                Console.Clear();
                Console.WriteLine($"Employee Control:  [{currentUser.Fname} - ID:{currentUser.Id}]\n");
                Console.WriteLine("'quit' to exit application\n");

                Console.WriteLine("1. EDIT: Update your own details\n");
                
                Console.WriteLine("5. UPDATE: Updates current employeeDB to file (SAVE&LOAD)\n");

                /*
                Console.ForegroundColor = ConsoleColor.DarkGray;                
                Console.WriteLine("\n\t\t\t(DEBUG)10. DataDump: Show all entries in database\n");
                Console.WriteLine("\t\t\t(DEBUG)20. Search: Find by employee id\n");
                Console.WriteLine("\t\t\t(DEBUG)30. Create: Creates new Employee\n");
                Console.WriteLine("\t\t\t(DEBUG)50. MakeAdmin: Give admin rights to an existing Employee\n");
                Console.WriteLine("\t\t\t(DEBUG)60. Delete: Deletes an existing Employee\n");
                Console.ResetColor();
                */

                menuChoice = Console.ReadLine();

                if (menuChoice == "1")
                {
                    Console.Clear();
                    var lookForId = currentUser.Id;
                    management.EditEmployee(employeeList, lookForId);
                }
                if (menuChoice == "5")
                {
                    management.SaveAllEmployeesToFile(employeeList);
                }

                if (menuChoice == "help")
                {
                    Console.Clear();
                    EmployeeMenu(employeeList);
                }
                /*
                if (menuChoice == "10")
                {
                    Console.Clear();
                    Console.WriteLine("DataDump:");
                    management.DataDump(employeeList);
                }
                if (menuChoice == "20")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id to SEARCH");
                    var searchId = management.FindEmployeeById(employeeList, lookForId);
                    Console.WriteLine(searchId.ToString());
                    Console.ReadKey();
                }
                if (menuChoice == "30")
                {
                    Console.Clear();
                    management.CreateEmployee(employeeList);
                }

                if (menuChoice == "50")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id to make ADMIN");
                    var searchId = management.FindEmployeeById(employeeList, lookForId);
                    management.MakeAdmin(employeeList, lookForId);
                    Console.WriteLine(searchId.ToString());
                    Console.WriteLine("<Any key>");
                    Console.ReadKey();

                }
                if (menuChoice == "60")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id for DELETE");
                    management.DeleteEmployee(employeeList, lookForId);
                }
                */
                
            }
        }
    }
}


