using Microsoft.EntityFrameworkCore;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;
using XBLog.Domain.Enum;

namespace XBlog.Infrastructure.Repository;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetActiveArticlesAsync()
    {
        return await _context.Articles.Include(a => a.ArticleCategory).Include(a => a.Comments).AsNoTracking()
            .Where(a => !a.IsDeleted).OrderByDescending(ac => ac.CreationDate).ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetAllArticleWithCategoryAsync()
    {
        return await _context.Articles.Include(a => a.ArticleCategory)
            .AsNoTracking().OrderByDescending(a => a.CreationDate).ToListAsync(); ;
    }

    public async Task<IEnumerable<Article>> GetAllArticleWithCategoryWithCommentsAsync()
    {
        return await _context.Articles.Where(a => !a.IsDeleted).Include(a => a.ArticleCategory).Include(a => a.Comments)
        .AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetArticleCreationTodayAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return await _context.Articles.Where(a => a.CreationDate >= today && a.CreationDate < tomorrow)
            .Include(a => a.ArticleCategory).AsNoTracking().ToListAsync();
    }

    public IQueryable<Article> GetArticleDetailsById(int id)
    {
        return _context.Articles
            .Where(a => !a.IsDeleted && a.Id == id).AsNoTracking();
    }

    public async Task<Article> GetArticleWithCategoryAndCommentsByIdAsync(int id)
    {
        var article = await _context.Articles.Include(a => a.ArticleCategory)
            .Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == id);
        if (article == null) return null;
        return article;
    }

    public async Task<IEnumerable<Article>> GetPopularArticlesAsync(int count = 3)
    {
        return await _context.Articles.Where(a => !a.IsDeleted)
            .OrderByDescending(a => a.Comments.Count(c => c.Status == StatusComment.Confirm))
            .Take(count).AsNoTracking().ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        if (article == null) return;
        article.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted);
        if (article == null) return;
        article.IsDeleted = false;
        await _context.SaveChangesAsync();
    }

}
