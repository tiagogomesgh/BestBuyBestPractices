using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void CreateProduct(string newName, double newPrice, int newCategoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
                new { name = newName, price = newPrice, categoryID = newCategoryID });
        }

        

        public void UpdateProduct(int productID, string newName)
        {
            _connection.Execute("UPDATE products SET Name = @newName WHERE ProductID = @productID;",
                new { newName = newName, productID = productID });
        }

       
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID",
                new { productID = productID });
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID",
                new { productID = productID });
        }
    }
}