using XBLog.Domain.Entities;

namespace XBLog.Domain.Contract;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task RemoveAsync(int id);
    Task RestoreAsync(int id);
    Task<IEnumerable<Article>> GetActiveArticlesAsync();
    Task<Article> GetArticleWithCategoryAndCommentsByIdAsync(int id);
    Task<IEnumerable<Article>> GetAllArticleWithCategoryAsync();
    Task<IEnumerable<Article>> GetAllArticleWithCategoryWithCommentsAsync();
    IQueryable<Article> GetArticleDetailsById(int id);
    Task<IEnumerable<Article>> GetArticleCreationTodayAsync();
    Task<IEnumerable<Article>> GetPopularArticlesAsync(int count=3);
}
