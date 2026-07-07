using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XBLog.Domain.Entities;

namespace XBlog.Infrastructure.Configs;

public class ArticleConfig : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.ArticleCategory).WithMany(ac => ac.Articles).HasForeignKey(a => a.ArticleCategoryId);
        builder.HasMany(a => a.Comments).WithOne(c => c.Article).HasForeignKey(c => c.ArticleId);
    }
}
