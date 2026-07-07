using XBlog.Application.Dto;


namespace XBlog.Presentation.Areas.Admin.Models;

public class AdminPanelViewModel
{
    public List<CommentDto> Comments { get; set; }
    public List<ArticleDto> Articles { get; set; }
    public List<ArticleCategoryDto> ArticleCategories { get; set; }
}
