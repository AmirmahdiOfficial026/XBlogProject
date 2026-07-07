using XBlog.Application.ApplicationMessage;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBLog.Domain.Contract;
using XBLog.Domain.Entities;
using XBLog.Domain.Enum;

namespace XBlog.Application.Serivces;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    public async Task<AppResult> AddArticleAsync(ArticleCreateDto dto)
    {
        try
        {
            var article = new Article
            {
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Image = dto.ImageUrl,
                Content = dto.Content,
                CreationDate = DateTime.Now,
                IsDeleted = false,
                ArticleCategoryId = dto.ArticleCategoryId,
            };
            await _articleRepository.AddAsync(article);
            return AppResult.IsSuccessed(ResultMessage.AddArticle);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<IEnumerable<ArticleDto>> GetActiveArticleAsync()
    {
        var articles = await _articleRepository.GetActiveArticlesAsync();
        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Image = a.Image,
            Content = a.Content,
            IsDeleted = a.IsDeleted,
            ArticleCategoryId = a.ArticleCategoryId,
            CreationDate = a.CreationDate,
            CategoryTitle = a.ArticleCategory.Title,
        });
    }

    public async Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
    {
        var articles = await _articleRepository.GetAllArticleWithCategoryAsync();
        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Image = a.Image,
            Content = a.Content,
            IsDeleted = a.IsDeleted,
            ArticleCategoryId = a.ArticleCategoryId,
            CreationDate = a.CreationDate,
            CategoryTitle = a.ArticleCategory.Title,
        });
    }

    public async Task<ArticleDto> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetArticleWithCategoryAndCommentsByIdAsync(id);
        if (article == null) return null;
        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            Image = article.Image,
            Content = article.Content,
            IsDeleted = article.IsDeleted,
            ArticleCategoryId = article.ArticleCategoryId,
            CreationDate = article.CreationDate,
            CategoryTitle = article.ArticleCategory.Title,
        };

    }

    public async Task<ArticleDetailsDto> GetArticleDetailByIdAsync(int id)
    {
        var query = _articleRepository.GetArticleDetailsById(id);
        return query.Select(a => new ArticleDetailsDto
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Image = a.Image,
            Content = a.Content,
            CreationDate = a.CreationDate,
            CategoryTitle = a.ArticleCategory.Title,
            CommentCount = a.Comments.Count(c => c.Status == XBLog.Domain.Enum.StatusComment.Confirm),
            Comments = a.Comments.Where(c => c.Status == StatusComment.Confirm)
                .OrderByDescending(c => c.CreationDate)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Message = c.Message,
                    CreationDate = c.CreationDate
                }).ToList(),
        }).FirstOrDefault();
    }

    public async Task<IEnumerable<ArticleDto>> GetArticleForMainSiteAsync(PaginationParams pagination)
    {
        var query = await _articleRepository.GetAllArticleWithCategoryWithCommentsAsync();
        var totalCount = query.Count();
        var articles = query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize).ToList();
        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Image = a.Image,
            Content = a.Content,
            IsDeleted = a.IsDeleted,
            ArticleCategoryId = a.ArticleCategoryId,
            CreationDate = a.CreationDate,
            CategoryTitle = a.ArticleCategory.Title,
            CommentCount = a.Comments.Count(c => c.Status == XBLog.Domain.Enum.StatusComment.Confirm),
        }).OrderByDescending(c => c.CreationDate).ToList();
    }

    public async Task<IEnumerable<ArticleDto>> GetArticlesCreationTodayAsync()
    {
        var articles = await _articleRepository.GetArticleCreationTodayAsync();
        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            IsDeleted = a.IsDeleted,
            CreationDate = a.CreationDate,
            CategoryTitle = a.ArticleCategory.Title,
        }).OrderByDescending(c => c.CreationDate).ToList();
    }

    public async Task<ArticleUpdateDto> GetArticleUpdateByIdAsync(int id)
    {
        var article = await _articleRepository.FindByIdAsync(id);
        if (article == null) return null;
        return new ArticleUpdateDto
        {
            Id = article.Id,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            ImageUrl = article.Image,
            Content = article.Content,
            ArticleCategoryId = article.ArticleCategoryId,
        };
    }

    public async Task<IEnumerable<PopularArticleDto>> GetPopularArticlesAsync(int count = 3)
    {
        var articles = await _articleRepository.GetPopularArticlesAsync(count);
        return articles.Select(a => new PopularArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            CreationDate = a.CreationDate,
            Image = a.Image
        }).ToList();
    }

    public async Task<AppResult> RemoveArticleAsync(int id)
    {
        try
        {
            await _articleRepository.RemoveAsync(id);
            return AppResult.IsSuccessed(ResultMessage.RemoveArticle);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<AppResult> RestoreArticleAsync(int id)
    {
        try
        {
            await _articleRepository.RestoreAsync(id);
            return AppResult.IsSuccessed(ResultMessage.RestoreArticle);
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }

    public async Task<AppResult> UpdateArticleAsync(ArticleUpdateDto dto)
    {
        try
        {
            var article = await _articleRepository.FindByIdAsync(dto.Id);
            if (article == null) return null;
            else
            {
                article.Title = dto.Title;
                article.ShortDescription = dto.ShortDescription;
                article.Image = dto.ImageUrl;
                article.Content = dto.Content;
                article.ArticleCategoryId = dto.ArticleCategoryId;
                await _articleRepository.UpdateAsync(article);
                return AppResult.IsSuccessed(ResultMessage.UpdateArticle);
            }
        }
        catch
        {
            return AppResult.IsFaulted(ResultMessage.NotFound);
        }
    }
}
