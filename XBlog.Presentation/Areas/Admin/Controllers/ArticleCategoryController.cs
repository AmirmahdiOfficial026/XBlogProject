using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBlog.Presentation.Areas.Admin.Models;

namespace XBlog.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        public ArticleCategoryController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var articllCategories = await _articleCategoryService.GetAllArticleCategoriesAsync();
            IEnumerable<ArticleCategoryModelView> ArticleCategories = articllCategories
                .Select(ac => new ArticleCategoryModelView
                {
                    Id = ac.Id,
                    Title = ac.Title,
                    CreationDate = ac.CreationDate,
                    IsDeleted = ac.IsDeleted
                }).OrderByDescending(ac => ac.CreationDate);
            return View(ArticleCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var articleCategoryCreate = new ArticleCategoryCreateDto();
            return View(articleCategoryCreate);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCategoryCreateDto articleCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _articleCategoryService.AddArticleCategoryAsync(articleCategory);
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
            var result = await _articleCategoryService.RemoveArticleCategoryDtoAsync(id);
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
            var result = await _articleCategoryService.RestoreArticleCategoryDtoAsync(id);
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
            var articleCategory = await _articleCategoryService.GetArticleCategoryUpdateDtoByIdAsync(id);
            return View(articleCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleCategoryUpdateDto articleCategory)
        {
            if (!ModelState.IsValid)
            {
                var articleCategoryInfo = await _articleCategoryService.GetArticleCategoryUpdateDtoByIdAsync(articleCategory.Id);
                return View(articleCategoryInfo);
            }
            var result = await _articleCategoryService.UpdateArticleCategoryDtoAsync(articleCategory);
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
