using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Seller
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Purse { get; set; }

    // Navigation Property
    public User User { get; set; }
}