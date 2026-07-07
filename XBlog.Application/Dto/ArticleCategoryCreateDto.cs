using System.ComponentModel.DataAnnotations;
using XBlog.Application.Extentions;

namespace XBlog.Application.Dto;

public class ArticleCategoryCreateDto
{
    [Required(ErrorMessage = "Title is required field!")]
    [ValidationField(false,true,true,ErrorMessage = "This field only accepts Persion letters and English letters")]
    public string Title { get; set; }
}
