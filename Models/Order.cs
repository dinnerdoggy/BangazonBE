// using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    // Navigation
    public Customer Customer { get; set; }
    public int SellerId { get; set; }
    // Navigation
    public Seller Seller { get; set; }
    public bool Pending { get; set; }
    public decimal Total { get; set; }

    // Products in the order - join table connection
    public List<ProductOrders> ProductOrders { get; set; }
}