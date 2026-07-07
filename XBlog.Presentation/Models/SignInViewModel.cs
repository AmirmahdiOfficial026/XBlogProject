using System.ComponentModel.DataAnnotations;

namespace XBlog.Presentation.Models;

public class SignInViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is InValid")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Comfirm Password is required")]
    [Compare(nameof(Password), ErrorMessage = "Is not compatible with your password.")]
    public string RePassword { get; set; }
}
