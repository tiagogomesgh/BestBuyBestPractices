using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            

            var departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name");

            var newDepartment = Console.ReadLine();

            departmentRepo.InsertDepartment(newDepartment);

            var departments = departmentRepo.GetAllDepartments();

            foreach (var dep in departments)
            {
                Console.WriteLine(dep.Name);
            }

            

            var productRepo = new DapperProductRepository(conn);

            Console.WriteLine("Add a new department to the database:");
            Console.WriteLine();
            Console.Write("Department Name:");
            var pName = Console.ReadLine();
            Console.Write("Product Price:");
            var pPrice = double.Parse(Console.ReadLine());
            Console.Write("Product category:");
            var pCatID = int.Parse(Console.ReadLine());

            Console.WriteLine($"Inserting new product '{pName}'");

            productRepo.CreateProduct(pName, pPrice, pCatID);

            var products = productRepo.GetAllProducts();

            foreach (var p in products)
            {
                Console.WriteLine(p.ProductID);
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Price);
                Console.WriteLine(p.CategoryID);
                Console.WriteLine(p.OnSale);
                Console.WriteLine(p.StockLevel);
            }


        }
    }
}