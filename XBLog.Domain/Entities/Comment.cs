using XBLog.Domain.Enum;

namespace XBLog.Domain.Entities;

public class Comment : BaseClass
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public StatusComment Status { get; set; }
    public Article Article { get; set; }
    public int ArticleId { get; set; }
}
