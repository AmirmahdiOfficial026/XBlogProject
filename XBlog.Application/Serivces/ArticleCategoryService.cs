using XBlog.Application.ApplicationMessage;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBlog.Application.Extentions;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;

namespace XBlog.Application.Serivces;

public class ArticleCategoryService : IArticleCategoryService
{
    private readonly IArticleCategoryRepository _articleCategoryRepository;
    public ArticleCategoryService(IArticleCategoryRepository articleCategoryRepository)
    {
        _articleCategoryRepository = articleCategoryRepository;
    }
    public async Task<AppResult> AddArticleCategoryAsync(ArticleCategoryCreateDto dto)
    {
        try
        {
            ArticleCategory articleCategory = new ArticleCategory
            {
                Title = dto.Title,
                CreationDate = DateTime.Now,
                IsDeleted = false,
            };
            await _articleCategoryRepository.AddAsync(articleCategory);
            return AppResult.IsSuccessed(ResultMessage.AddArticleCategory);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }

    }

    public async Task<IEnumerable<ArticleCategoryDto>> GetActiveArticleCategoryDtoAsync()
    {
        var articleCategories = await _articleCategoryRepository.GetActiveArticleCategoriesAsync();
        return articleCategories.Select(ac => new ArticleCategoryDto
        {
            Id = ac.Id,
            Title = ac.Title,
            CreationDate = ac.CreationDate,
            IsDeleted = ac.IsDeleted,
        }).ToList();
    }

    public async Task<IEnumerable<ArticleCategoryDto>> GetAllArticleCategoriesAsync()
    {
        var articleCategories = await _articleCategoryRepository.GetAllAsync();
        return articleCategories.Select(ac => new ArticleCategoryDto
        {
            Id = ac.Id,
            Title = ac.Title,
            CreationDate = ac.CreationDate,
            IsDeleted = ac.IsDeleted,
        }).ToList();
    }

    public async Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int id)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(id);
        if (articleCategory == null) return null;
        return new ArticleCategoryDto
        {
            Id = articleCategory.Id,
            CreationDate = articleCategory.CreationDate,
            Title = articleCategory.Title,
            IsDeleted = articleCategory.IsDeleted,
        };
    }

    public async Task<ArticleCategoryUpdateDto> GetArticleCategoryUpdateDtoByIdAsync(int id)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(id);
        if (articleCategory == null) return null;
        return new ArticleCategoryUpdateDto
        {
            Id = articleCategory.Id,
            Title = articleCategory.Title,
        };
    }

    public async Task<AppResult> RemoveArticleCategoryDtoAsync(int id)
    {
        try
        {
            await _articleCategoryRepository.RemoveAsync(id);
            return AppResult.IsSuccessed(ResultMessage.RemoveArticleCategory);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<AppResult> RestoreArticleCategoryDtoAsync(int id)
    {
        try
        {
            await _articleCategoryRepository.RestoreAsync(id);
            return AppResult.IsSuccessed(ResultMessage.RestoreArticleCategory);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<AppResult> UpdateArticleCategoryDtoAsync(ArticleCategoryUpdateDto dto)
    {
        try
        {
            var articleCategory = await _articleCategoryRepository.FindByIdAsync(dto.Id);
            if (articleCategory == null) return AppResult.IsFaulted(ResultMessage.ArticleCategoryNotFound);
            else
            {
                articleCategory.Title = dto.Title;
                await _articleCategoryRepository.UpdateAsync(articleCategory);
                return AppResult.IsSuccessed(ResultMessage.UpdateArticleCategory);
            }
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }
}
