using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public string Description { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }
    public int? Quantity { get; set; }

    // Navigation Property
    public ProductType ProductType { get; set; }
}