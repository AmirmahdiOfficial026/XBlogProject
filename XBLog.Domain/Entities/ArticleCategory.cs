namespace XBLog.Domain.Entities;
public class ArticleCategory : BaseClass
{
    public string Title { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<Article> Articles { get; set; } = new List<Article>();
}
