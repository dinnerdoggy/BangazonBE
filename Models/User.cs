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

// DTO for CheckUser
public class CheckUserDto
{
    public string Uid { get; set; }
}

// DTO for registering users
public class RegisterUserDto
{
    public string Uid { get; set; }
    public string UserName { get; set; }
}