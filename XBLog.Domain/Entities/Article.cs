namespace XBLog.Domain.Entities;

public class Article : BaseClass
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Image { get; set; }
    public string Content { get; set; }
    public bool IsDeleted { get; set; }
    public ArticleCategory ArticleCategory { get; set; }
    public int ArticleCategoryId { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
}
