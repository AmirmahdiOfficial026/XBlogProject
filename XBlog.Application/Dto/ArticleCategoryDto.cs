namespace XBlog.Application.Dto;

public class ArticleCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreationDate { get; set; }
}
