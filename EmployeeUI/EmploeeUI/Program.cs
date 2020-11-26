using System;
using EmployeeUI;
using System.IO;
using SharedLibrary;

namespace EmployeeUI.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeUI employee = new EmployeeUI();
            employee.EmployeeLogon();
        }
    }
}
