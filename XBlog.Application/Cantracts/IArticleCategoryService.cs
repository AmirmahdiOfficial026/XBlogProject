using XBlog.Application.ApplicationMessage;
using XBlog.Application.Dto;

namespace XBlog.Application.Cantracts;

public interface IArticleCategoryService
{
    Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int id);
    Task<IEnumerable<ArticleCategoryDto>> GetAllArticleCategoriesAsync();
    Task<IEnumerable<ArticleCategoryDto>> GetActiveArticleCategoryDtoAsync();
    Task<ArticleCategoryUpdateDto> GetArticleCategoryUpdateDtoByIdAsync(int id);
    Task<AppResult> AddArticleCategoryAsync(ArticleCategoryCreateDto dto);
    Task<AppResult> UpdateArticleCategoryDtoAsync(ArticleCategoryUpdateDto dto);
    Task<AppResult> RemoveArticleCategoryDtoAsync(int id);
    Task<AppResult> RestoreArticleCategoryDtoAsync(int id);
}
