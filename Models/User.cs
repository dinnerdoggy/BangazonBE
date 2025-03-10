using System.ComponentModel.DataAnnotations;

namespace BangazonBE.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Uid { get; set; }
    [Required]
    public string UserName { get; set; }
}