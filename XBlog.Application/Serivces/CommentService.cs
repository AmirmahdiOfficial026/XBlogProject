using XBlog.Application.ApplicationMessage;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;

namespace XBlog.Application.Serivces;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<AppResult> AddCommentAsync(CreateCommentDto dto)
    {
        try
        {
            var comment = new Comment
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                Status = XBLog.Domain.Enum.StatusComment.Pending,
                ArticleId = dto.ArticleId,
                CreationDate = DateTime.Now,
            };
            await _commentRepository.AddAsync(comment);
            return AppResult.IsSuccessed(ResultMessage.AddComment);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public AppResult ComfirmComment(int id)
    {
        try
        {
            _commentRepository.ConfirmComment(id);
            return AppResult.IsSuccessed(ResultMessage.ConfirmComment);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<CommentDto> GetCommentByIdAsync(int id)
    {
        var comment = await _commentRepository.FindByIdAsync(id);
        if (comment == null) return null;
        return new CommentDto
        {
            Id = comment.Id,
            Name = comment.Name,
            Message = comment.Message,
            Email = comment.Email,
            Status = comment.Status,
            ArticleId = comment.ArticleId,
            CreationDate = comment.CreationDate,
        };
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsAsync()
    {
        var comments = await _commentRepository.GetCommentsAsync();
        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Name = c.Name,
            Message = c.Message,
            Email = c.Email,
            ArticleId = c.ArticleId,
            CreationDate = c.CreationDate,
            Status = c.Status,
            ArticleTitle = c.Article.Title
        }).OrderByDescending(c => c.CreationDate).ToList();
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByArticleIdAsync(int articleId)
    {
        var comments = await _commentRepository.GetCommentByArticlesIdAsync(articleId);
        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Name = c.Name,
            Message = c.Message,
            Email = c.Email,
            ArticleId = c.ArticleId,
            CreationDate = c.CreationDate,
            Status = c.Status,
            ArticleTitle = c.Article.Title,
        }).ToList();
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsCreationTodayAsync()
    {
        var comments = await _commentRepository.GetCommnetCreationTodayAsync();
        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Name = c.Name,
            Message = c.Message,
            Email = c.Email,
            CreationDate = c.CreationDate,
            Status = c.Status,
            ArticleTitle = c.Article.Title
        }).OrderByDescending(c=>c.CreationDate).ToList();
    }

    public AppResult RejectComment(int id)
    {
        try
        {
            _commentRepository.RejectComment(id);
            return AppResult.IsSuccessed(ResultMessage.RejectComment);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public AppResult RemoveComment(int id)
    {
        try
        {
            _commentRepository.DeleteComment(id);
            return AppResult.IsSuccessed(ResultMessage.RemoveComment);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }
}
