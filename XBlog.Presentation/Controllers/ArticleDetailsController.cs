using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;
using XBlog.Presentation.Models;
using XBLog.Domain.Entities;

namespace XBlog.Presentation.Controllers
{
    public class ArticleDetailsController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        public ArticleDetailsController(IArticleService articleService, ICommentService commentService)
        {
            _articleService = articleService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ArticleDetail(int id)
        {

            ArticleDetailsDto Article = await _articleService.GetArticleDetailByIdAsync(id);
            ViewBag.Article = Article;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                ArticleDetailsDto Article = await _articleService.GetArticleDetailByIdAsync(dto.ArticleId);
                ViewBag.Article = Article;
                return View("ArticleDetail");
            }
            var result = await _commentService.AddCommentAsync(dto);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("ArticleDetail", new { id = dto.ArticleId });
        }
    }
}
