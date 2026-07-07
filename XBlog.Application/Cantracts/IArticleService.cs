using XBlog.Application.ApplicationMessage;
using XBlog.Application.Dto;

namespace XBlog.Application.Cantracts;

public interface IArticleService
{
    Task<ArticleDto> GetArticleByIdAsync(int id);
    Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
    Task<IEnumerable<ArticleDto>> GetActiveArticleAsync();
    Task<AppResult> AddArticleAsync(ArticleCreateDto dto);
    Task<AppResult> RemoveArticleAsync(int id);
    Task<AppResult> RestoreArticleAsync(int id);
    Task<AppResult> UpdateArticleAsync(ArticleUpdateDto dto);
    Task<ArticleUpdateDto> GetArticleUpdateByIdAsync(int id);
    Task<ArticleDetailsDto> GetArticleDetailByIdAsync(int id);
    Task<IEnumerable<ArticleDto>> GetArticleForMainSiteAsync(PaginationParams pagination);
    Task<IEnumerable<ArticleDto>> GetArticlesCreationTodayAsync();
    Task<IEnumerable<PopularArticleDto>> GetPopularArticlesAsync(int count = 3);
}
