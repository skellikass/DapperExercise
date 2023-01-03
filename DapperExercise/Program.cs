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

            #region Departments
            DapperDeptRepo deptRepo = new DapperDeptRepo(conn);
            IEnumerable<Department> departments = deptRepo.GetAllDepartments();

            Console.WriteLine("Here is a list of current departments at Best Buy:");
            foreach (var dept in departments)
            {
                Console.WriteLine($"Dept ID: {dept.DepartmentID}, Dept Name: {dept.Name}");
            }
            Console.WriteLine("Please create a new department for Best Buy by entering the name below:");
            string userDept = Console.ReadLine();
            deptRepo.InsertDepartment(userDept);
            IEnumerable<Department> updatedDepts = deptRepo.GetAllDepartments();
            Console.WriteLine("Great idea!  Here is the updated list of departments:");
            foreach (var dept in updatedDepts)
            {
                Console.WriteLine($"Dept ID: {dept.DepartmentID}, Dept Name: {dept.Name}");
            }
            #endregion

            #region Products
            DapperProductRepo prodRepo = new DapperProductRepo(conn);
            prodRepo.CreateProduct("Coruscat: RealMe", 7.99, 7);
            IEnumerable<Product> products = prodRepo.GetAllProducts();
            Console.WriteLine("Here is a list of current products and their prices:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Name}, Product Price: {product.Price}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            prodRepo.UpdateProductStockLevel("Coruscat: RealMe", 100);
            IEnumerable<Product> updatedProducts1 = prodRepo.GetAllProducts();
            foreach (var product in updatedProducts1)
            {
                Console.WriteLine($"Product Name: {product.Name}, Stock Level: {product.StockLevel}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            prodRepo.DeleteProduct("Coruscat: RealMe");
            IEnumerable<Product> updatedProducts2 = prodRepo.GetAllProducts();
            foreach (var product in updatedProducts2)
            {
                Console.WriteLine($"Product Name: {product.Name}, Product Price: {product.Price}");
            }
            #endregion
        }
    }
}