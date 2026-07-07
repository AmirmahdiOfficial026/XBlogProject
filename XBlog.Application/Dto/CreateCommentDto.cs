using System.ComponentModel.DataAnnotations;

namespace XBlog.Application.Dto;

public class CreateCommentDto
{
    [Required(ErrorMessage = "Name is required!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress(ErrorMessage = "Email is Invalid!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Message is required!")]
    public string Message { get; set; }
    [Required(ErrorMessage = "Article is required!")]
    public int ArticleId { get; set; }
}
