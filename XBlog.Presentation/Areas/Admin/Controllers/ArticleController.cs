using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBlog.Presentation.Areas.Admin.Models;
namespace XBlog.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly ICludeinaryService _cludeinaryService;
        public ArticleController(IArticleService articleService, ICludeinaryService cludeinaryService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _cludeinaryService = cludeinaryService;
            _articleCategoryService = articleCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var articleCategories = await _articleCategoryService.GetActiveArticleCategoryDtoAsync();
            ArticleCreateModelView article = new ArticleCreateModelView()
            {
                categories = articleCategories.Select(ac => new SelectListItem(ac.Title, ac.Id.ToString())).ToList(),
            };
            return View(article);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateDto article)
        {
            if (!ModelState.IsValid)
            {
                var articleCategories = await _articleCategoryService.GetActiveArticleCategoryDtoAsync();
                var categories = articleCategories
                .Select(ac => new SelectListItem(ac.Title, ac.Id.ToString())).ToList();

                ArticleCreateModelView articleCreate = new ArticleCreateModelView()
                {
                    categories = categories,
                };
                return View(articleCreate);
            }
            var imageUrl = await _cludeinaryService.UploadImageAsync(article.Image, "XBLog");
            article.ImageUrl = imageUrl;
            var result = await _articleService.AddArticleAsync(article);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _articleService.RemoveArticleAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Active(int id)
        {
            var result = await _articleService.RestoreArticleAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ArticleUpdateDto article = await _articleService.GetArticleUpdateByIdAsync(id);
            var articleCategories = await _articleCategoryService.GetActiveArticleCategoryDtoAsync();
            if (article == null) return RedirectToAction("Index");
            ViewBag.Categories = articleCategories.Select(ac => new SelectListItem(ac.Title, ac.Id.ToString())).ToList();
            return View(article);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var articleCategories = await _articleCategoryService.GetActiveArticleCategoryDtoAsync();
                ViewBag.Categories = articleCategories
                    .Select(ac => new SelectListItem(ac.Title, ac.Id.ToString())).ToList();
                return View(dto);
            }
            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(dto.ImageUrl) && dto.ImageUrl != "Not Image")
                {
                    await _cludeinaryService.DeleteImageAsync(dto.ImageUrl);
                }
                var newItemUrl = await _cludeinaryService.UploadImageAsync(dto.Image, "XBLog");
                dto.ImageUrl = newItemUrl;
            }
            else { }
            var result = await _articleService.UpdateArticleAsync(dto);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Index");

        }
    }
}
