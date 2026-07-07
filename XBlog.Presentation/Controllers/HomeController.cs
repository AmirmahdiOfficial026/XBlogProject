using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XBlog.Application.Cantracts;
using XBlog.Presentation.Models;

namespace XBlog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var articles = await _articleService.GetArticleForMainSiteAsync(new PaginationParams
            {
                PageNumber = page,
                PageSize = 10,
            });
            ViewBag.TotalCount = (await _articleService.GetAllArticlesAsync()).Count();
            ViewBag.CurrentPage = page;
            return View(articles);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
