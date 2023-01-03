using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace DapperExercise
{
    internal class Program
    {
        static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);
            DapperDeptRepo repo = new DapperDeptRepo(conn);
            IEnumerable<Department> departments = repo.GetAllDepartments();
            Console.WriteLine("Here is a list of current departments at Best Buy:");
            foreach (var dept in departments)
            {
                Console.WriteLine($"Dept ID: {dept.DepartmentID}, Dept Name: {dept.Name}");
            }
            Console.WriteLine("Please create a new department for Best Buy by entering the name below:");
            string userDept = Console.ReadLine();
            repo.InsertDepartment(userDept);
            IEnumerable<Department> updatedDepts = repo.GetAllDepartments();
            Console.WriteLine("Great idea!  Here is the updated list of departments:");
            foreach (var dept in updatedDepts)
            {
                Console.WriteLine($"Dept ID: {dept.DepartmentID}, Dept Name: {dept.Name}");
            }
        }
    }
}