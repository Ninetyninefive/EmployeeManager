using System;
using AdminUI;
using System.IO;
using SharedLibrary;


namespace AdminUI.Program
{
    public class Program
    {
        public Program()
        {
            
        }

        private static void Main(string[] args)
        {
            AdminUI admin = new AdminUI();
            admin.AdminLogon();
        }
    }
}
