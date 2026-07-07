using Microsoft.EntityFrameworkCore;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;

namespace XBlog.Infrastructure.Repository;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly AppDbContext _context;
    public CommentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public void ConfirmComment(int id)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return;
        comment.Status = XBLog.Domain.Enum.StatusComment.Confirm;
        _context.SaveChanges();
    }

    public void DeleteComment(int id)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return;
        _context.Remove(comment);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<Comment>> GetCommentByArticlesIdAsync(int articleId)
    {
        return await _context.Comments.Where(c => c.ArticleId == articleId)
            .OrderByDescending(c => c.CreationDate).ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        return await _context.Comments.Include(c => c.Article).ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommnetCreationTodayAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return await _context.Comments.Where(c => c.CreationDate >= today && c.CreationDate<tomorrow)
            .Include(c => c.Article).AsNoTracking().ToListAsync();
    }

    public void RejectComment(int id)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return;
        comment.Status = XBLog.Domain.Enum.StatusComment.Reject;
        _context.SaveChanges();
    }
}