using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XBLog.Domain.Entities;

namespace XBlog.Infrastructure.Configs;

public class ArticleCategoryConfig : IEntityTypeConfiguration<ArticleCategory>
{
    public void Configure(EntityTypeBuilder<ArticleCategory> builder)
    {
        builder.HasKey(ac => ac.Id);
        builder.HasMany(ac => ac.Articles).WithOne(a => a.ArticleCategory).HasForeignKey(a => a.ArticleCategoryId);
    }
}
