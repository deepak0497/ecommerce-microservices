using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.DTOs;
using ProductService.Models;

namespace ProductService.Controllers
{

        [ApiController]
        [Route("api/products")]
        public class ProductsController : ControllerBase
        {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductDbContext _context;
            public ProductsController(ProductDbContext context , ILogger<ProductsController> logger)
            {
             _logger = logger;
            _context = context;
            }
        [HttpGet]
        public IActionResult GetProducts()
        {      

            _logger.LogInformation("Retrieving all products from the database.");
            var products = _context.Products.ToList();
            return Ok(products);

            // Logic to retrieve products from the database
            //  return Ok(new[] { new { Id = Guid.NewGuid(), Name = "Sample Product", Price = 9.99m, Stock = 100, Description = "This is a sample product.", CreatedAt = DateTime.UtcNow } });
        }

        [HttpGet("{id}")]  
            public IActionResult GetProduct(Guid id)

            {

                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
                // Logic to retrieve products from the database
                //  return Ok(new[] { new { Id = Guid.NewGuid(), Name = "Sample Product", Price = 9.99m, Stock = 100, Description = "This is a sample product.", CreatedAt = DateTime.UtcNow } });
            }
        
           [HttpPost]
            public IActionResult CreateProduct(CreateProductDto dto)
            {
                // Logic to create a new product in the database
                var product = new Product
                { Id = Guid.NewGuid(), Name = dto.Name, Price = dto.Price, Stock = dto.Stock, Description = dto.Description, CreatedAt = DateTime.UtcNow };
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok(product);
            }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
            public IActionResult DeleteProduct(Guid id)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Product Deleted Successfully");
            }
        }
    }

