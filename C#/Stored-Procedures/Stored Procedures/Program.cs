using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Stored_Procedures
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseFunctions dbFunction = new DatabaseFunctions();

            string input = "Stored Procedures";

            while (input != "quit")
            {
                Console.WriteLine(
                    "\n \nSQL Stored Procedures. Press following number to execute them:" + "\n" +
                    "1. Get all departments" + "\n" +
                    "2. Get department" + "\n" +
                    "3. Create Department" + "\n" +
                    "4. Update department name" + "\n" +
                    "5. Update Department Manager" + "\n" +
                    "6. Delete Department");

                input = Console.ReadLine();

                if (input == "1")
                {
                    dbFunction.GetAllDepartments();
                }
                if (input == "2")
                {
                    Console.WriteLine("Department number:");
                    var departmentNumber = Int32.Parse(Console.ReadLine());
                    dbFunction.GetDepartment(departmentNumber);
                }
                if (input == "3")
                {
                    Console.WriteLine("New department's name:");
                    string departmentName = Console.ReadLine();

                    Console.WriteLine("New department's manager SSN:");
                    string managerSSN = Console.ReadLine();

                    dbFunction.CreateDepartment(departmentName, managerSSN);
                }
                if (input == "4")
                {
                    Console.WriteLine("Department's new name:");
                    string departmentName = Console.ReadLine();

                    Console.WriteLine("Department number:");
                    int departmentNumber = Int32.Parse(Console.ReadLine());

                    dbFunction.UpdateDepartmentName(departmentName, departmentNumber);
                }
                if (input == "5")
                {
                    Console.WriteLine("Department number:");
                    int departmentNumber = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Department's new manager SSN (Press 1 if you want to see employee SSNs):");
                    string managerSSN = Console.ReadLine();
                    if (managerSSN == "1")
                    {
                        dbFunction.GetEmployeeNamesAndSSN();

                        Console.WriteLine("Department's new manager SSN:");
                        managerSSN = Console.ReadLine();
                    }

                    dbFunction.UpdateDepartmentManager(departmentNumber, managerSSN);
                }
                if (input == "6")
                {
                    Console.WriteLine("Department number:");
                    int departmentNumber = Int32.Parse(Console.ReadLine());

                    dbFunction.DeleteDepartment(departmentNumber);
                }
            }
        }
    }
}
