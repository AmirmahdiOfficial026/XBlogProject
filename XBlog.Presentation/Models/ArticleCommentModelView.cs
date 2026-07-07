using XBlog.Application.Dto;

namespace XBlog.Presentation.Models
{
    public class ArticleCommentModelView
    {
        public ArticleDetailsDto Article { get; set; }
        public CreateCommentDto Comment { get; set; }
    }
}
