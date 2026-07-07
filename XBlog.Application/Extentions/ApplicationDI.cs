using Microsoft.Extensions.DependencyInjection;
using XBlog.Application.Cantracts;
using XBlog.Application.Serivces;

namespace XBlog.Application.Extentions;

public static class ApplicationDI
{
    public static IServiceCollection AddAplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICludeinaryService, CludeInaryService>();
        services.AddScoped<ICommentService, CommentService>();
        return services;
    }
}
