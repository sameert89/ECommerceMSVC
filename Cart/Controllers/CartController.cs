using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cart.Models;

namespace Cart.Controllers
{
    [ApiController]
    [Route("customer-api/v1/")]
    public class CartController : ControllerBase
    {
        private Dictionary<string, Order> orders;

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(string id)
        {
            if (orders.TryGetValue(id, out Order order))
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult<string> Add()
    }
}
