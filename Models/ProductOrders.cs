namespace BangazonBE.Models;

public class ProductOrders
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    // Navigation
    public Order Order { get; set; }
    public int ProductId { get; set; }
    // Navigation
    public Product Product { get; set; }

    public int Quantity { get; set; }
}

public class ProductOrderDto
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}
