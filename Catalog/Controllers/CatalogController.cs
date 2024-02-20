using Catalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("catalog-api/v1/")]
    public class CatalogController : Controller
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product(1, "Laptop", "A high performance laptop", 1200.00m),
            new Product(2, "Smartphone", "An advanced smartphone", 800.00m),
            new Product(3, "Tablet", "A user-friendly tablet for all your needs", 600.00m),
        };

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
