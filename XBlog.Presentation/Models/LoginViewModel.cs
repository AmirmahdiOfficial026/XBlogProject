using System.ComponentModel.DataAnnotations;

namespace XBlog.Presentation.Models;
public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress(ErrorMessage ="Email is inValid!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; }
    public bool RemeberMe { get; set; }
}
