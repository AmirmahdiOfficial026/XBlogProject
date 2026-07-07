using XBLog.Domain.Entities;

namespace XBLog.Domain.Contract;

public interface IArticleCategoryRepository : IBaseRepository<ArticleCategory>
{
    Task RemoveAsync(int id);
    Task RestoreAsync(int id);
    Task<IEnumerable<ArticleCategory>> GetActiveArticleCategoriesAsync();
}
