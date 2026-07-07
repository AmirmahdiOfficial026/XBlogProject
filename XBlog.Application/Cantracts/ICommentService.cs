using XBlog.Application.ApplicationMessage;
using XBlog.Application.Dto;

namespace XBlog.Application.Cantracts;

public interface ICommentService
{
    Task<AppResult> AddCommentAsync(CreateCommentDto dto);
    AppResult RemoveComment(int id);
    AppResult RejectComment(int id);
    AppResult ComfirmComment(int id);
    Task<IEnumerable<CommentDto>> GetCommentsAsync();
    Task<IEnumerable<CommentDto>> GetCommentsByArticleIdAsync(int articleId);
    Task<CommentDto> GetCommentByIdAsync(int id);
    Task<IEnumerable<CommentDto>> GetCommentsCreationTodayAsync();
}
