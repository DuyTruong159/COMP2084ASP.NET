using LabWebSecurity.Controllers;
using LabWebSecurity.Data;
using LabWebSecurity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LabWebApp.Tests
{
    public class ProductTests
    {
        private ApplicationDbContext _context;
        private ProductsController _controller;
        public ProductTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use InMemoryDatabase for testing
                .Options;
            _context = new ApplicationDbContext(options);
            _controller = new ProductsController(_context);
        }

        [Fact]
        public void Product_PropertiesSetCorrectly()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test product description",
                Price = 9.99m,
            };

            // Act

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal("Test product description", product.Description);
            Assert.Equal(9.99m, product.Price);
        }

        [Fact]
        public void Product_ReturnView()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test product description",
                Price = 9.99m,
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            // Act
            var result = _controller.Details(product.Id).Result as ViewResult;
            var model = result?.Model as Product;

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(1, model.Id);
        }
    }
}