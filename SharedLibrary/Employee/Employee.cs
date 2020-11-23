using System;
namespace SharedLibrary
{
    public class Employee
    {
        
        private string  _id;
        private string  _password;

        private string  _admin;

        private string  _fname;
        private string  _lname;
        private string  _email;

        private string  _address;

        private string  _position;
        private string  _salary;

        public Employee()
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }

        public Employee(string Id, string Password, string Admin)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }

        public Employee(string Id, string Password, string Admin,
                            string Fname)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }
        public Employee(string Id, string Password, string Admin,
                            string Fname, string Lname)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }
        public Employee(string Id, string Password, string Admin,
                            string Fname, string Lname, string Email)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }
        public Employee(string Id, string Password, string Admin,
                            string Fname, string Lname, string Email,
                                string Address)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }
        public Employee(string Id, string Password, string Admin,
                            string Fname, string Lname, string Email,
                                string Address, string Position)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }
        public Employee( string Id, string Password, string Admin,
                            string Fname, string Lname, string Email,
                                string Address, string Position, string Salary)
        {
            //ID
            _id = Id;
            _password = Password;

            _admin = Admin;
            _fname = Fname;
            _lname = Lname;

            _email = Email;

            _address = Address;

            _position = Position;
            _salary = Salary;

        }


        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        public string Admin
        {
            get { return _admin; }
            set
            {
                if (value == "1")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{Id} // {Name} is an Admin.");
                    Console.ResetColor();
                }
                if (value == "0")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{Id} // {Name} is not an Admin.");
                    Console.ResetColor();
                }
            }
        }

        public string Fname
        {
            get { return _fname; }
            set
            {
                _fname = value;
            }
        }

        public string Lname
        {
            get { return _lname; }
            set
            {
                _lname = value;
            }
        }

        public string Name
        {
            get { return (_fname + ' ' + _lname); }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }

        public string Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
            }
        }

        public override string ToString() => Id + "," + Password + "," + Admin + "," + Fname + "," + Lname + "," + Email + "," + Address + "," + Position + "," + Salary + ";";


        public void ViewEmployee()
        {
        }

    }
}
