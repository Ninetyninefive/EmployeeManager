using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharedLibrary
{
    public enum EmployeeFields { Id, password, admin, fname, lname, email, address, posiion, salary }

    public class EmployeeManager
    {

        //public static string _filename = Directory.GetCurrentDirectory() + "employeeDB.csv";
        //public override string ToString() => Id + "," + Password + "," + Admin + "," + Fname + "," + Lname + "," + Email + "," + Address + "," + Position + "," + Salary + ";";
        //public static string _path = "EmployeeDB";
        public static string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string _filepath = _path + "/employees.csv";

        public EmployeeManager()
        {
        }

        public Employee ValidateEmployee(List<Employee> employeeList)
        {
            var idToValidate = ValidateInput("Employee ID");
            var passwordToValidate = ValidateInput("Password");

            foreach (var item in employeeList)
            {
                if (idToValidate == item.Id && passwordToValidate == item.Password && item.Admin == "1")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Admin Validation -- Success!");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Welcome back, {item.Name} :ID#{item.Id}!");
                    Console.ResetColor();
                    return item;
                }
                if (idToValidate == item.Id && passwordToValidate == item.Password)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Employee Validation -- Success!");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Welcome back, {item.Name} :ID#{item.Id}!");
                    Console.ResetColor();
                    return item;
                }

            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Employee Validation -- Fail!");
            Console.ResetColor();
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
                var countEmployees = 0;
                var countLoads = 0;
                var database = file.ReadToEnd();
                database.Replace('\n', ';');
                var singleEmployee = database.Split(';');

                foreach (var item in singleEmployee)
                {
                    countEmployees++;
                    var employeeAttr = item.Split(',') ;

                    if (employeeAttr.Length < 3)
                    {
                        continue;
                    }
                    for (int i = 0; i < employeeAttr.Count(); i++)
                    {
                        Console.WriteLine($"<<--##{countEmployees} ## {(EmployeeFields)i} ## {employeeAttr[i]}##-->>");
                    }
                    Employee loadedEmployee = new Employee(employeeAttr[0].Trim(), employeeAttr[1].TrimStart().TrimEnd(), employeeAttr[2].TrimStart().TrimEnd(), employeeAttr[3].TrimStart().TrimEnd(), employeeAttr[4].TrimStart().TrimEnd(), employeeAttr[5].TrimStart().TrimEnd(), employeeAttr[6].TrimStart().TrimEnd(), employeeAttr[7].TrimStart().TrimEnd(), employeeAttr[8].TrimStart().TrimEnd());
                    foreach (var existing in employeeListFromFile)
                    {
                        if(loadedEmployee == existing)
                        {
                            continue;
                        }
                    }
                    if (!employeeListFromFile.Contains(loadedEmployee))
                    {
                        if (!(loadedEmployee.Id == "") && !(loadedEmployee.Password == "") && !(loadedEmployee.Admin == ""))
                        {
                            employeeListFromFile.Add(loadedEmployee);
                            countLoads++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Duplication handled.");
                        Console.ResetColor();
                    }
                }
                file.Close();
                Console.WriteLine($"DB_Load was success! ({countLoads} imported entries)\n<Any Key>");
                Console.ReadKey();

            }
            return employeeListFromFile;
        }

        public void SaveAllEmployeesToFile(List<Employee> employeeList)
        {
            var counter = 0;
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
                        if (!(item.Id == "") && !(item.Password == "") && !(item.Admin == ""))
                        {
                            sb.Append(item.ToString().TrimStart().TrimEnd() + ';');
                            counter++;
                        }
                    }
                    file.WriteLine(sb);
                    file.Close();
                    Console.WriteLine($"DB_Save was success! ({counter} saved entries)\n<Any Key>");
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
                        if (!(item.Id == "") && !(item.Password == "") && !(item.Admin == ""))
                        {
                            sb.Append(item.ToString());
                            counter++;
                        }
                    }
                    Console.WriteLine(sb);
                    file.Close();
                    Console.WriteLine("<Any key>");
                    Console.ReadKey();
                }
            }
        }

        public string DisplayFromFile()
        {
            Console.WriteLine("Shows entries in files tha are not in working memory:");

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
                var countEmployees = 0;
                var countLoads = 0;
                var database = file.ReadToEnd();
                database.Replace('\n', ';');
                var singleEmployee = database.Split(';');

                foreach (var item in singleEmployee)
                {
                    countEmployees++;
                    var employeeAttr = item.Split(',');

                    if (employeeAttr.Length < 3)
                    {
                        continue;
                    }
                    Employee loadedEmployee = new Employee(employeeAttr[0].Trim(), employeeAttr[1].TrimStart().TrimEnd(), employeeAttr[2].TrimStart().TrimEnd(), employeeAttr[3].TrimStart().TrimEnd(), employeeAttr[4].TrimStart().TrimEnd(), employeeAttr[5].TrimStart().TrimEnd(), employeeAttr[6].TrimStart().TrimEnd(), employeeAttr[7].TrimStart().TrimEnd(), employeeAttr[8].TrimStart().TrimEnd());
                    foreach (var existing in employeeListFromFile)
                    {
                        if (loadedEmployee == existing)
                        {
                            continue;
                        }
                    }
                    if (!employeeListFromFile.Contains(loadedEmployee))
                    {
                        if (!(loadedEmployee.Id == "") && !(loadedEmployee.Password == "") && !(loadedEmployee.Admin == ""))
                        {
                            employeeListFromFile.Add(loadedEmployee);
                            countLoads++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Duplication handled.");
                        Console.ResetColor();
                    }
                }
                file.Close();


                foreach (var item in employeeListFromFile)
                {
                    item.ToString();
                }
                Console.WriteLine($"DB_Show was success! ({countLoads} entries)\n<Any Key>");
                Console.ReadKey();

            }
            return "Done with fileduump";
        }
        public List<Employee> UpdateDB(List<Employee> employeeList)
        {
            SaveAllEmployeesToFile(employeeList);
            var newEmployeeList = TryLoadEmployeesFromFile();

            return newEmployeeList;
        }


        public string ValidateInput(string reason)
        {
            Console.WriteLine($"Enter {reason}:\t\t'help' for <help> or 'quit' to <quit>\n");
            var input = Console.ReadLine();
            if (input == "quit")
            {
                Environment.Exit(-1);
            }
            if (input == "help")
            {
                Console.WriteLine("<HELP>: Type 'quit' to exit.");
            }
            while (input == "" || (input.Contains(',')) || (input.Contains(';')))
            {
                Console.WriteLine($"Try again -- Enter new {reason}:");
                input = Console.ReadLine();
            }
            return input;
        }

        public bool CheckForExistingId(List<Employee> employeeList, string lookForId)
        {
            foreach (var item in employeeList)
            {
                if (lookForId == item.Id)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ID collision -- this ID already exists!");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Found: ID# {item.Id} (Name:{item.Name})!");
                    Console.ResetColor();
                    return true;
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("No employee has given id!");
            Console.ResetColor();
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
                var newEmployeeList = UpdateDB(employeeList);
                //SaveToCSV(employeeList);

                return newEmployeeList;
            }
            catch (Exception ex)
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
                    var newEmployeeList = UpdateDB(employeeList);
                    return newEmployeeList;
                }
            }
            return employeeList;
        }

        public List<Employee> EditEmployee(List<Employee> employeeList, string lookForId)
        {

            Employee employeeToEdit = FindEmployeeById(employeeList, lookForId);
            Employee oldInfo = new Employee();
            oldInfo = employeeToEdit;

            Console.WriteLine("Employee to edit:");
            Console.WriteLine(employeeToEdit.ToString());

            Console.WriteLine($"current:{oldInfo.Password}");
            employeeToEdit.Password = ValidateInput("new Password");
            if (employeeToEdit.Admin == "1")
            {
                Console.WriteLine($"current:{oldInfo.Admin}");
                employeeToEdit.Admin = ValidateInput("Still Admin?");
            }
            Console.WriteLine($"current:{oldInfo.Fname}");
            employeeToEdit.Fname = ValidateInput("First name");
            Console.WriteLine($"current:{oldInfo.Lname}");
            employeeToEdit.Lname = ValidateInput("Last name");
            Console.WriteLine($"current:{oldInfo.Email}");
            employeeToEdit.Email = ValidateInput("Email");
            Console.WriteLine($"current:{oldInfo.Address}");
            employeeToEdit.Address = ValidateInput("Address");
            Console.WriteLine($"current:{oldInfo.Position}");
            employeeToEdit.Position = ValidateInput("Position");
            Console.WriteLine($"current:{oldInfo.Salary}");
            employeeToEdit.Salary = ValidateInput("Salary");

            
            employeeList.Remove(oldInfo);
            employeeList.Add(employeeToEdit);
            var newEmployeeList = UpdateDB(employeeList);
            //SaveToCSV(employeeList);

            return newEmployeeList;
        }

        public Employee FindEmployeeById(List<Employee> employeeList, string lookForId)
        {
            foreach (var item in employeeList)
            {
                if (lookForId == item.Id)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Success!");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Found: ID# {item.Id} (Name:{item.Name})!");
                    Console.ResetColor();
                    return item;
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("No employee has given id -- Fail!");
            Console.ResetColor();
            return new Employee();
        }

        public void DataDump(List<Employee> employeeList)
        {
            Console.WriteLine("Entries in working memory:");
            foreach (Employee employee in employeeList)
            {
                Console.WriteLine(employee.ToString());
            }
            Console.WriteLine("<Any key>");
            Console.ReadKey();

            Console.WriteLine("Entries in working memory:");
            Console.WriteLine(DisplayFromFile());

            Console.WriteLine("<Any key>");
            Console.ReadKey();
        }

        public List<Employee> MakeAdmin(List<Employee> employeeList, string lookForId)
        {

            Employee employeeToAdmin = FindEmployeeById(employeeList, lookForId);
            Employee oldInfo = new Employee();
            oldInfo = employeeToAdmin;

            Console.WriteLine("Employee to edit:");
            Console.WriteLine(employeeToAdmin.ToString());
            employeeToAdmin.Admin = ValidateInput("Admin");

            Console.WriteLine("Changes performed:");
            Console.WriteLine(oldInfo.ToString());
            Console.WriteLine(employeeToAdmin.ToString());

            employeeList.Remove(oldInfo);
            employeeList.Add(employeeToAdmin);
            var newEmployeeList = UpdateDB(employeeList);
            //SaveToCSV(employeeList);

            return newEmployeeList;
        }

        public List<Employee> DeleteEmployee(List<Employee> employeeList, string lookForId)
        {
            Employee EmployeeToDelete = FindEmployeeById(employeeList, lookForId);
            if(EmployeeToDelete.Id == lookForId)
            {
                employeeList.Remove(EmployeeToDelete);
                var newEmployeeList = UpdateDB(employeeList);
                return newEmployeeList;
            }
            return employeeList;
        }


    }
}
