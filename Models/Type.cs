using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Type
{
    public int Id { get; set; }
    [Required]
    public string TypeName { get; set; }

    // Navigation Property
    public List<Product> Products { get; set; } = new List<Product>();
}