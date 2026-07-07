namespace XBlog.Application.Dto;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Image { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreationDate { get; set; }
    public int ArticleCategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public int CommentCount { get; set; }
}
