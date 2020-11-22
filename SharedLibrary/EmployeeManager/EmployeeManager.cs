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
                if (idToValidate == item.Id && passwordToValidate == item.Password)
                {
                    Console.WriteLine("Employee Validation -- Success!");
                    return item;
                }
                if (idToValidate == item.Id && passwordToValidate == item.Password && item.Admin == "1")
                {
                    Console.WriteLine("Admin Validation -- Success!");
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

            if(!File.Exists(_filepath))
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
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            List<Employee> employeeList = new List<Employee>();

            try
            {
                    FileStream file = File.Open(_filepath, FileMode.Open);
                    string[] temployee = _filepath.Split('\n');
                    file.Close();
                    string[] sb = new string[9];
                    foreach (var entry in temployee)
                    {
                       sb = entry.Split(',');
                       Employee loadedEmployee = new Employee(sb[0], sb[1], sb[2], sb[3], sb[4], sb[5], sb[6], sb[7], sb[8]);
                       employeeList.Add(loadedEmployee);
                    }
                return employeeList;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong when loading employee database @ >> {_path}");
            }
            return employeeList;
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
                        sb.Append(item.ToString());
                    }
                    file.WriteLine(sb);
                    file.Close();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong when saving database @ >> {_path}");
            }
        }
        


        public string ValidateInput(string reason)
        {
            Console.WriteLine("'help' for <help>\n");
            Console.WriteLine($"Enter {reason}:");
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
                var col = CheckForExistingId(employeeList, newID);
                while (col)
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
                    Console.WriteLine("Admin found.");
                    return employeeList;
                }
                else
                {
                    Employee root = new Employee("001", "password", "1");
                    employeeList.Add(root);
                    SaveAllEmployeesToFile(employeeList);
                    //SaveToCSV(employeeList);

                    return employeeList;
                    
                }
            }
            return employeeList;
        }

        public List<Employee> EditEmployee(List<Employee> employeeList, string lookForId)
        {

            Employee employeeToEdit = GetEmployeeById(employeeList, lookForId);

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
                    return (new Employee());
            }
            return null;
        }

        public void DataDump(List<Employee> employeeList)
        {
                foreach (Employee employee in employeeList)
                {
                    Console.WriteLine(employee.ToString());
                }   
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
            }
            catch (Exception ex)
            {
                return "Something went wrong when deleting employee";
            }

        }
    }
}
