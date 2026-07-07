using Microsoft.EntityFrameworkCore;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;

namespace XBlog.Infrastructure.Repository;

public class ArticleCategoryRepository : BaseRepository<ArticleCategory>, IArticleCategoryRepository
{
    private readonly AppDbContext _context;

    public ArticleCategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ArticleCategory>> GetActiveArticleCategoriesAsync()
    {
        return await _context.ArticleCategories
            .Where(ac => !ac.IsDeleted)
            .OrderByDescending(ac => ac.CreationDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var articleCategory = await _context.ArticleCategories.FirstOrDefaultAsync(ac => ac.Id == id&&!ac.IsDeleted);
        if (articleCategory == null) return;
        articleCategory.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var articleCategory = await _context.ArticleCategories.FirstOrDefaultAsync(ac => ac.Id == id&& ac.IsDeleted);
        if (articleCategory == null) return;
        articleCategory.IsDeleted = false;
        await _context.SaveChangesAsync();
    }
}
