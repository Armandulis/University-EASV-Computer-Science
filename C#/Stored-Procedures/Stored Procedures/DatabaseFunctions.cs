using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Stored_Procedures
{
    public class DatabaseFunctions
    {
        SqlConnection sqlCon;

        public DatabaseFunctions()
        {
            // Syntax of sql server connection string
            sqlCon = new SqlConnection("Server=.\\;Database=Company;Integrated Security=true");
        }
        internal void GetAllDepartments()
        {

            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_GetAllDepartments", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Read Results
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nDepartments: \n");
            while (reader.Read())
            {
                // Show all results to the user
                Console.WriteLine("DName: " + reader["DName"] + ", " +
                    "DNumber: " + reader["DNumber"].ToString() + ", " +
                    "MgrSSN: " + reader["MgrSSN"] + ", " +
                    "MgrStartDate: " + reader["MgrStartDate"] + ", " +
                    "EmployeeAmount: " + reader["EmployeeAmount"].ToString());
            }

            reader.Close();
            sqlCon.Close();
        }

        internal void GetDepartment(int departmentNumber)
        {

            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_GetDepartment", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set procedure's parameters
            cmd.Parameters.Add(new SqlParameter("@dnumber", departmentNumber));

            try
            {
                // Read Results
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nDepartment number: " + departmentNumber + " \n");
                while (reader.Read())
                {
                    // Show all results to the user
                    Console.WriteLine("DName: " + reader["DName"] + ", " +
                        "DNumber: " + reader["DNumber"].ToString() + ", " +
                        "MgrSSN: " + reader["MgrSSN"] + ", " +
                        "MgrStartDate: " + reader["MgrStartDate"] + ", " +
                        "EmployeeAmount: " + reader["EmployeeAmount"].ToString());
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + " Could not get department: " + departmentNumber + " " + e.Message + "\n");
            }
            sqlCon.Close();
        }

        internal void CreateDepartment(string departmentName, string managerSSN)
        {
            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_CreateDepartment", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set procedure's parameters
            cmd.Parameters.Add(new SqlParameter("@departurename", departmentName));
            cmd.Parameters.Add(new SqlParameter("@mgrSSN", managerSSN));

            try
            {
                // Read Results
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nDepartment number with name: " + departmentName + " and manager: " + managerSSN + ":\n");
                while (reader.Read())
                {
                    // Show all results to the user
                    Console.WriteLine(reader["DNumber"].ToString());
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + " Could not create department: " + e.Message + "\n");
            }
            sqlCon.Close();
        }

        internal void UpdateDepartmentName(string departmentName, int departmentNumber)
        {
            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_UpdateDepartmentName", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set procedure's parameters
            cmd.Parameters.Add(new SqlParameter("@departurename", departmentName));
            cmd.Parameters.Add(new SqlParameter("@dnumber", departmentNumber));

            try
            {
                // Execute query
                cmd.ExecuteNonQuery();

                // Inform user
                Console.WriteLine("\nDepartment " + departmentNumber + " now has a new name: " + departmentName);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + " Could not update department's name: " + e.Message + "\n");
            }
            sqlCon.Close();
        }

        internal void DeleteDepartment(int departmentNumber)
        {
            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_DeleteDepartment", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set procedure's parameters
            cmd.Parameters.Add(new SqlParameter("@dnumber", departmentNumber));
            try
            {
                // Execute query
                cmd.ExecuteNonQuery();

                // Inform user
                Console.WriteLine("\nDepartment " + departmentNumber + " was deleted along with associated project and locations, employees managers were updated as well");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + " Could not delete department" + departmentNumber + ": " + e.Message + "\n");
            }
            sqlCon.Close();
        }

        internal void UpdateDepartmentManager(int departmentNumber, string managerSSN)
        {
            // Call stored procedure
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("usp_UpdateDepartmentManager", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set procedure's parameters
            cmd.Parameters.Add(new SqlParameter("@mgrSSN", managerSSN));
            cmd.Parameters.Add(new SqlParameter("@dnumber", departmentNumber));

            try
            {
                // Execute query
                cmd.ExecuteNonQuery();

                // Inform user
                Console.WriteLine("\nDepartment " + departmentNumber + " now has a new manager with SSN: " + managerSSN);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + " Could not update manager: " + e.Message + "\n");
            }

            sqlCon.Close();
        }

        internal void GetEmployeeNamesAndSSN()
        {
            // Form and execute query
            sqlCon.Open();
            SqlCommand command = new SqlCommand("SELECT TOP (100) [FName], [SSN], [SuperSSN], [Dno] FROM [Company].[dbo].[Employee]", sqlCon);
            SqlDataReader reader = command.ExecuteReader();

            // Show results to the user
            Console.WriteLine("\nEmployees \n");
            while (reader.Read())
            {
                Console.WriteLine("Name: " + reader["FName"] + ", SNN: " + reader["SSN"] + ", SuperSSN: " + reader["SuperSSN"] + ", Dno: " + reader["Dno"]);
            }
            reader.Close();
            sqlCon.Close();
        }
    }
}
