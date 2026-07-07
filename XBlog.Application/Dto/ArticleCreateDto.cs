using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace XBlog.Application.Dto;

public class ArticleCreateDto
{
    [Required(ErrorMessage = "Article title is required!")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Article ShortDescription is required!")]
    public string ShortDescription { get; set; }
    [Required(ErrorMessage = "Article Image is required!")]
    public IFormFile Image { get; set; }
    public string ImageUrl { get; set; } = "Not Image";
    [Required(ErrorMessage = "Article Content is required!")]
    public string Content { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Article CategoryTitle is required!")]
    public int ArticleCategoryId { get; set; }
}
