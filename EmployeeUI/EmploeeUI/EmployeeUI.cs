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
            Console.WriteLine("Hello and welcome to the Employee UI of EmployeeManager\nRoot@[001:password]");
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

                menuChoice = management.ValidateInput("Menu choice");

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
                
            }
        }
    }
}


