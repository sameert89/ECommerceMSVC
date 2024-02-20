using Microsoft.AspNetCore.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly CustomerModel _dummyCustomer = new CustomerModel();
        [HttpGet]
        public ActionResult<CustomerModel> GetCustomerModel()
        {
            return _dummyCustomer;
        }
        [HttpPost]
        public IActionResult BuyProduct()
        {
            // Get the product from the Catalog
            // Make call to the cart using the ID
        }
    }
}
