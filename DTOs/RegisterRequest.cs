using System.ComponentModel.DataAnnotations;

namespace WebApplication3.DTOs;

public class RegisterRequest
{
    [Required(ErrorMessage = "Login is required")]
    [MinLength(3, ErrorMessage = "Login must be at least 3 characters long")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Name is required")]
    
    public string Name { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }
}