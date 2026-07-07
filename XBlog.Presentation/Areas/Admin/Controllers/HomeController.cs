using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBlog.Application.Cantracts;
using XBlog.Presentation.Areas.Admin.Models;

namespace XBlog.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HomeController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        public HomeController(IArticleCategoryService articleCategoryService, IArticleService articleService, ICommentService commentService)
        {
            _articleCategoryService = articleCategoryService;
            _articleService = articleService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var articleCategories = await _articleCategoryService.GetAllArticleCategoriesAsync();
            var article = await _articleService.GetArticlesCreationTodayAsync();
            var comments = await _commentService.GetCommentsCreationTodayAsync();
            AdminPanelViewModel model = new AdminPanelViewModel()
            {
                ArticleCategories = articleCategories.ToList(),
                Articles = article.ToList(),
                Comments = comments.ToList(),
            };
            return View(model);
        }
    }
}
