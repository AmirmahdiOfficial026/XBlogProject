using Microsoft.AspNetCore.Mvc;
using XBlog.Application.Cantracts;

namespace XBlog.Presentation.Components;

public class PopularArticlesViewComponent : ViewComponent
{
    private readonly IArticleService _articleService;
    public PopularArticlesViewComponent(IArticleService articleService)
    {
        _articleService = articleService;
    }
    public async Task<IViewComponentResult> InvokeAsync(int count = 3)
    {
        var popularArticles = await _articleService.GetPopularArticlesAsync(count);
        return View(popularArticles);
    }
}
