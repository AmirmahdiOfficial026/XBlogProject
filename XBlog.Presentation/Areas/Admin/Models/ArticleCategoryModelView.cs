namespace XBlog.Presentation.Areas.Admin.Models
{
    public class ArticleCategoryModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
