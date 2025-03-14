using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class Customer
{
    public int Id { get; set; }
    public int UserId { get; set; }

    // Navigation property
    public User User { get; set; }
}