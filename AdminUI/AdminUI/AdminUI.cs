using System;
using System.Collections.Generic;
using System.IO;
using SharedLibrary;


namespace AdminUI
{
    class AdminUI
    {
        Employee root = new Employee("001", "password", "1", "root", "rootson", "rootmail", "rootvägen", "CEO of MEGAROOT", "All the munnies!");
        public Employee currentUser = new Employee();
        public EmployeeManager management = new EmployeeManager();
        public bool LoggedOn = false;
        


        public void AdminLogon()
        {
            Console.Clear();
            Console.WriteLine("SETUP>>");
            List<Employee> employeeList = management.TryLoadEmployeesFromFile();

            //employeeList.Add(root);
            
            management.FindAdminOrRoot(employeeList);

            Console.Clear();
            Console.WriteLine("Hello and welcome to the AdminUI of EmployeeManager\n");
            while (!LoggedOn)
            {
            var validAdmin = management.ValidateEmployee(employeeList);
            if (validAdmin.Admin == "1")
            {
                currentUser = validAdmin;

                Console.WriteLine("You successfully logged on as an Admin!");
                LoggedOn = true;
            }

            else
            {
                Console.WriteLine("Login as Admin unsuccessful...");
            }
            }
            if (LoggedOn)
            {
            AdminMenu(employeeList);
            }
        }
        
        public void AdminMenu(List<Employee> employeeList)
        {
            var menuChoice = Console.ReadLine();
            while (menuChoice != "quit")
            {
                Console.Clear();
                Console.WriteLine("Admin Control:\n");


                Console.WriteLine("1. DataDump: Show all entries in database\n");
                Console.WriteLine("2. Create: Creates new Employee\n");
                Console.WriteLine("3. EditEmployee: Select user by id and change it's information\n");
                Console.WriteLine("4. Search: Find by employee id\n");
                Console.WriteLine("5. MakeAdmin: Give admin rights to an existing Employee\n");
                Console.WriteLine("6. DeleteUser: Deletes an existing Employee\n");

                Console.WriteLine("9. UPDATE: Updates current employeeDB to file (SAVE&LOAD)");

                menuChoice = Console.ReadLine();

                if (menuChoice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("DataDump:");
                    management.DataDump(employeeList);
                }
                if (menuChoice == "2")
                {
                    Console.Clear();
                    management.CreateEmployee(employeeList);
                }
                if (menuChoice == "3")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id to EDIT");
                    management.EditEmployee(employeeList, lookForId);
                }
                if (menuChoice == "4")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id to SEARCH");
                    var searchID = management.GetEmployeeById(employeeList, lookForId);
                    if (searchID == null)
                    {
                        Console.WriteLine("Error during lookup. <Any key>");
                        Console.ReadKey();
                    }
                    else
                        Console.WriteLine(searchID.ToString());
                        Console.WriteLine("<Any key>");
                        Console.ReadKey();
                }
                if (menuChoice == "5")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id to make ADMIN");
                    management.MakeAdmin(employeeList, lookForId);
                }
                if (menuChoice == "6")
                {
                    Console.Clear();
                    var lookForId = management.ValidateInput("Id for DELETE");
                    management.DeleteEmployee(employeeList, lookForId);
                }
                if (menuChoice == "8")
                {
                    employeeList = management.TryLoadEmployeesFromFile();   
                }
                if (menuChoice == "9")
                {
                    management.SaveAllEmployeesToFile(employeeList);
                }

                if (menuChoice == "help")
                {
                    Console.Clear();
                    AdminMenu(employeeList);
                }
            }
        }
    }
}


