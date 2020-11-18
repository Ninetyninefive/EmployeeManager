using System;


namespace AdminUI
{
    class AdminUI
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var result = SharedLibrary.EmployeeManager.StartsWithUpper("Hello");
            Console.WriteLine(result);
            Console.ReadKey();

        }
    }
}
