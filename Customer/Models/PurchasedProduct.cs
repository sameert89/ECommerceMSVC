using Microsoft.AspNetCore.Components.Forms;

namespace Customer.Models
{
    public class PurchasedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public PurchasedProduct(int id, string name, string description, decimal price, int qty)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = qty;
        }
    }
}
