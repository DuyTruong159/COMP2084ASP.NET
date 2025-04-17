using DotNetDrinksWebUI.Controllers;
using DotNetDrinksWebUI.Data;
using DotNetDrinksWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetDrinksWebUI.Tests
{
    public class ProductEditTest
    {
        private ApplicationDbContext _context;
        private ProductsController _controller;

        public ProductEditTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use InMemoryDatabase for testing
                .Options;
            _context = new ApplicationDbContext(options);
            _controller = new ProductsController(_context);

            //_context.Products.Add(new Product { Id = 3, Name = "Test Product" });
            //_context.SaveChanges();
        }

        [Fact]
        public void Product_CheckViewNameEdit()
        {
            _context.Products.Add(new Product { Id = 3, Name = "Test Product" });
            _context.SaveChanges();

            var result = _controller.Edit(3)?.Result as ViewResult;
            Assert.Equal("Edit", result?.ViewName);
        }

        [Fact]
        public void Product_CheckDeleteConfirmSuccesfully()
        {
            var result = _controller.DeleteConfirmed(3);
            var deletedProduct = _context.Products.Find(3);
            
            Assert.Null(deletedProduct);
        }
    }
}