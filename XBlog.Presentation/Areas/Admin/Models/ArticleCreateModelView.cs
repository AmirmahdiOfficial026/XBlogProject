using Microsoft.AspNetCore.Mvc.Rendering;
using XBlog.Application.Dto;

namespace XBlog.Presentation.Areas.Admin.Models
{
    public class ArticleCreateModelView
    {
        public ArticleCreateDto Article { get; set; }
        public List<SelectListItem> categories { get; set; }
    }
}
