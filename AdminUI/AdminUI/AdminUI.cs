using System;
using System.Collections.Generic;
using System.IO;
using SharedLibrary;


namespace AdminUI
{
    class AdminUI
    {
        public Employee currentUser = new Employee();
        public Employee selectedUser = new Employee();
        public EmployeeManager management = new EmployeeManager();
        public bool LoggedOn = false;


        public void AdminLogon()
        {
            //List<Employee> employeeList = management.TryLoadEmployeesFromFile();
            List<Employee> employeeList = management.LoadFromCSV();

            management.FindAdminOrRoot(employeeList);

            foreach (var employee in employeeList)
            {
                employee.ToString();
            }

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

                menuChoice = Console.ReadLine();

                if (menuChoice == "1")
                {
                    management.DataDump(employeeList);
                }
                if (menuChoice == "2")
                {
                    management.CreateEmployee(employeeList);
                }
                if (menuChoice == "3")
                {
                    var lookForId = management.ValidateInput("Id to EDIT");
                    management.EditEmployee(employeeList, lookForId);
                }
                if (menuChoice == "5")
                {
                    var lookForId = management.ValidateInput("Id to make ADMIN");
                    management.MakeAdmin(employeeList, lookForId);
                }
                if (menuChoice == "6")
                {
                    var lookForId = management.ValidateInput("Id for DELETE");
                    management.DeleteEmployee(employeeList, lookForId);
                }
            }
        }
    }
}


