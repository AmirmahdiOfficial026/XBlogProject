namespace XBlog.Application.Dto;

public class ArticleDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string CategoryTitle { get; set; }
    public DateTime CreationDate { get; set; }
    public int CommentCount { get; set; }
    public List<CommentDto> Comments { get; set; }
}
