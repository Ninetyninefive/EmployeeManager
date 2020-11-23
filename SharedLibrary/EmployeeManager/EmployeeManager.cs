using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharedLibrary
{
    public class EmployeeManager
    {

        //public static string _filename = Directory.GetCurrentDirectory() + "employeeDB.csv";

        public static string _path = "EmployeeDB";
        public static string _filepath = _path + "/employees.csv";
        public EmployeeManager()
        {
        }

        public Employee ValidateEmployee(List<Employee> employeeList)
        {
            var idToValidate = ValidateInput("Employee ID");
            var passwordToValidate = ValidateInput("Password");
            var empArray = employeeList.ToArray();
            foreach (var item in empArray)
            {
                if (idToValidate == item.Id && passwordToValidate == item.Password && item.Admin == "1")
                {
                    Console.WriteLine("Admin Validation -- Success!");
                    return item;
                }
                if (idToValidate == item.Id && passwordToValidate == item.Password)
                {
                    Console.WriteLine("Employee Validation -- Success!");
                    return item;
                }

            }
            Console.WriteLine("Employee Validation -- Fail!");
            return new Employee();
        }

        public List<Employee> LoadFromCSV()
        {
            List<Employee> employeeList = new List<Employee>();

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            if (!File.Exists(_filepath))
            {
                File.Create(_filepath);
            }

            TextReader textReader = File.OpenText(_filepath);
            CsvHelper.CsvReader csv = new CsvHelper.CsvReader((CsvHelper.IParser)textReader);

            var employees = csv.GetRecords<Employee>();

            foreach (var employee in employees)
            {
                employeeList.Add(employee);
            }
            textReader.Close();

            return employeeList;
        }

        public void SaveToCSV(List<Employee> employeeList)
        {
            TextWriter textWriter = File.CreateText(_filepath);

            CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter((CsvHelper.ISerializer)textWriter);
            csvWriter.WriteRecords(employeeList);

            textWriter.Close();
        }


        public List<Employee> TryLoadEmployeesFromFile()
        {
            List<Employee> employeeListFromFile = new List<Employee>();

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            if (!File.Exists(_filepath))
            {
                File.Create(_filepath);
            }
            using (StreamReader file = File.OpenText(_filepath))
            {

                var database = file.ReadToEnd();
                
                var singleEmployee = database.Split(';');
                
                foreach (var item in singleEmployee)
                {
                    var employeeAttr = item.Split(',');

                    if (employeeAttr.Length < 3)
                    {
                        continue;
                    }
                        for(int i=0; i<employeeAttr.Count(); i++)
                        {
                        Console.WriteLine(employeeAttr[i]);
                        }
                        Employee loadedEmployee = new Employee(employeeAttr[0], employeeAttr[1], employeeAttr[2], employeeAttr[3], employeeAttr[4], employeeAttr[5], employeeAttr[6], employeeAttr[7], employeeAttr[8]);
                        if (!employeeListFromFile.Contains(loadedEmployee))
                        {
                            employeeListFromFile.Add(loadedEmployee);
                        }
                        else
                        {
                            Console.WriteLine("Duplication handled.");
                        }
                }
                file.Close();
                Console.WriteLine("DB_Load was success! <Any Key>");
                Console.ReadKey();
                
            }
            return employeeListFromFile;
        }

        public void SaveAllEmployeesToFile(List<Employee> employeeList)
        {
            try
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                using (StreamWriter file = File.CreateText(_filepath))
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var item in employeeList)
                    {
                            sb.Append(item.ToString() + Environment.NewLine);
                    }
                    file.WriteLine(sb);
                    file.Close();
                    Console.WriteLine("DB_Save was success! <Any Key>");
                    Console.ReadKey();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG:: Something went wrong when saving database @ >> {_path}");
                using (StreamWriter file = File.CreateText(_filepath))
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var item in employeeList)
                    {
                        sb.Append(item.ToString() + Environment.NewLine);
                    }
                    Console.WriteLine(sb);
                    file.Close();

                    Console.WriteLine("<Any key>");
                    Console.ReadKey();
                }
            }
        }
        


        public string ValidateInput(string reason)
        {
            Console.WriteLine($"Enter {reason}:\t'help' for <help> or 'quit' to <quit>\n");
            var input = Console.ReadLine();
            if (input == "quit")
            {
                Environment.Exit(-1);
            }
            if (input == "help")
            {
                Console.WriteLine("<HELP>: Type 'quit' to exit.");
            }
            while (input == "" || (input.Contains(',')))
            {
                Console.WriteLine($"Try again -- Enter new {reason}:");
                input = Console.ReadLine();
            }
            return input;
        }

        public bool CheckForExistingId(List<Employee> employeeList, string newID)
        {
            var empArray = employeeList.ToArray();
            foreach (var item in empArray)
            {
                if (newID == item.Id)
                {
                    Console.WriteLine("ID collision -- this ID already exists!");
                    return true;
                }
            }
            return false;
        }
        public List<Employee> CreateEmployee(List<Employee> employeeList)
        {
            try
            {
                var newID = ValidateInput("Employee Id");
                var collidingID = CheckForExistingId(employeeList, newID);
                while (collidingID)
                {
                    newID = ValidateInput("Employee Id");
                }
                var newPassword = ValidateInput("Password");
                var newAdmin = ValidateInput("Admin");
                var newFname = ValidateInput("First name");
                var newLname = ValidateInput("Last name");
                var newEmail = ValidateInput("Email");
                var newAddress = ValidateInput("Address");
                var newPosition = ValidateInput("Position");
                var newSalary = ValidateInput("Salary");
                Employee newEmployee = new Employee(newID, newPassword, newAdmin, newFname, newLname, newEmail, newAddress, newPosition, newSalary);
                employeeList.Add(newEmployee);
                SaveAllEmployeesToFile(employeeList);
                //SaveToCSV(employeeList);

                return employeeList;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Something went wrong when creating new employee.");
            }
            return employeeList;
        }
        public List<Employee> FindAdminOrRoot(List<Employee> employeeList)
        {
            var empArray = employeeList.ToArray();
            foreach (var item in empArray)
            {
                if (item.Admin == "1")
                {
                    Console.WriteLine("Admin found in database.");
                    return employeeList;
                }
                else
                {
                    Console.WriteLine("Adding root~!");
                    Employee root = new Employee("001", "password", "1", "root", "rootson", "rootmail", "rootvägen", "CEO of MEGAROOT", "All the munnies!");
                    employeeList.Add(root);
                    //SaveToCSV(employeeList);
                    return employeeList;
                }
            }
            return employeeList;
        }

        public List<Employee> EditEmployee(List<Employee> employeeList, string lookForId)
        {

            Employee employeeToEdit = GetEmployeeById(employeeList, lookForId);

            Console.WriteLine("Employee to edit:");
            Console.WriteLine(employeeToEdit.ToString());
            
            employeeToEdit.Password = ValidateInput("Password");
            if (employeeToEdit.Admin == "1")
            {
                employeeToEdit.Admin = ValidateInput("Admin");
            }
            employeeToEdit.Fname = ValidateInput("First name");
            employeeToEdit.Lname = ValidateInput("Last name");
            employeeToEdit.Email = ValidateInput("Email");
            employeeToEdit.Address = ValidateInput("Address");
            employeeToEdit.Position = ValidateInput("Position");
            employeeToEdit.Salary = ValidateInput("Salary");

            employeeList.Add(employeeToEdit);
            SaveAllEmployeesToFile(employeeList);
            //SaveToCSV(employeeList);

            return employeeList;
        }

        public Employee GetEmployeeById(List<Employee> employeeList, string lookForId)
        {
            foreach (Employee employee in employeeList)
            {
                if (employee.Id == lookForId)
                {
                    Console.WriteLine("Found match:\n");
                    Console.WriteLine(employee.ToString());
                    return employee;
                }
                else
                    return null;
            }
            return null;
        }

        public void DataDump(List<Employee> employeeList)
        {
                foreach (Employee employee in employeeList)
                {
                    Console.WriteLine(employee.ToString());
                }
            Console.WriteLine("<Any key>");
            Console.ReadKey();
        }

        public string MakeAdmin(List<Employee> employeeList, string lookForId)
        {
            try
            {
                Employee selectedEmployee = GetEmployeeById(employeeList, lookForId);
                selectedEmployee.Admin = "1";
                employeeList.Add(selectedEmployee);
                SaveAllEmployeesToFile(employeeList);
                //SaveToCSV(employeeList);
                return "Success!";
            }
            catch (Exception ex)
            {
                return "Something went when making Admin";
            }
        }

        public string DeleteEmployee(List<Employee> employeeList, string lookForId)
        {
            try
            {
                Employee selectedEmployee = GetEmployeeById(employeeList, lookForId);
                employeeList.Remove(selectedEmployee);
                SaveAllEmployeesToFile(employeeList);
                //SaveToCSV(employeeList);
                return "Success!";
                Console.WriteLine("<Any key>");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                return "Something went wrong when deleting employee";
                Console.WriteLine("<Any key>");
                Console.ReadKey();
            }

        }
    }
}
