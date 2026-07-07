using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XBlog.Infrastructure.Repository;
using XBLog.Domain.Contract;

namespace XBlog.Infrastructure.Extentions;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        service.AddScoped<IArticleRepository, ArticleRepository>();
        service.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
        service.AddScoped<ICommentRepository, CommentRepository>();
        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return service;
    }
}
