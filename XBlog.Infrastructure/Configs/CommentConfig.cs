using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XBLog.Domain.Entities;

namespace XBlog.Infrastructure.Configs;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
    }
}
