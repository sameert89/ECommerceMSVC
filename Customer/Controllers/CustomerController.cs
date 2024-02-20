using Microsoft.AspNetCore.Mvc;
using Customer.Models;
using Catalog.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace Customer.Controllers
{
    [ApiController]
    [Route("customer-api/v1/")]
    public class CustomerController : Controller
    {
        private static readonly CustomerModel _dummyCustomer = new();
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CustomerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult<CustomerModel> GetCustomerModel()
        {
            return _dummyCustomer;
        }
        [HttpPost]
        public void BuyProduct(int id, int qty)
        {
            var cartURI = _configuration.GetValue<string>("NeighborURIs:CartService");
            var catalogURI = _configuration.GetValue<string>("NeighborURIs:CatalogService");
            Product? product = null;
            // Get the product from the Catalog
            try
            {
                HttpResponseMessage catalogResponse = _httpClient.GetAsync($"{catalogURI}/{id}").Result;

                catalogResponse.EnsureSuccessStatusCode();

                catalogResponse.Content.ReadAsStringAsync().ContinueWith(readTask =>
                {
                    string JsonString = readTask.Result; 
                    product = JsonSerializer.Deserialize<Product>(JsonString);
                });
                if(product == null) {
                    throw new Exception("Unable to read product from Catalog");
                }
                // Create a View for the Cart MSVC
                var purchasedProduct = new Dictionary<string, string?>
                {
                    ["Id"] = product.Id.ToString(),
                    ["Name"] = product.Name,
                    ["Description"] = product.Description,
                    ["Price"] = product.Price.ToString(),
                    ["Quantity"] = qty.ToString()
                };

                // Make POST call to the cart to add items
                HttpResponseMessage cartResponse = _httpClient.GetAsync(QueryHelpers.AddQueryString(cartURI, purchasedProduct)).Result;

                cartResponse.EnsureSuccessStatusCode();

                cartResponse.Content.ReadAsStringAsync().ContinueWith(readTask =>
                {
                    Console.WriteLine($"Item Added to Order ID: {readTask.Result}");
                });

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error: ", e.Message);
            }
        }
    }
}
