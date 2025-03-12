using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.Entities.User;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string? EMail { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(10)]
    public string? Password { get; set; }
}


