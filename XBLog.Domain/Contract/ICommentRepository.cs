using XBLog.Domain.Entities;

namespace XBLog.Domain.Contract;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentByArticlesIdAsync(int articleId);
    Task<IEnumerable<Comment>> GetCommentsAsync();
    Task<IEnumerable<Comment>> GetCommnetCreationTodayAsync();
    void ConfirmComment(int id);
    void RejectComment(int id);
    void DeleteComment(int id);
}
