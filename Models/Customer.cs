using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Customer
{
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }

    // Navigation property
    public User User { get; set; }
}