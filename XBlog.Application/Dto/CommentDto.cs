using XBLog.Domain.Enum;

namespace XBlog.Application.Dto;

public class CommentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public StatusComment Status { get; set; }
    public string ArticleTitle { get; set; }
    public int ArticleId { get; set; }
    public DateTime CreationDate { get; set; }
}
