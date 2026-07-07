using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBlog.Application.Cantracts;
using XBlog.Application.Dto;

namespace XBlog.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var comment = await _commentService.GetCommentsAsync();
            return View(comment);
        }
        [HttpPost]
        public IActionResult Confirm(int id)
        {
            var result = _commentService.ComfirmComment(id);
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
        public IActionResult Reject(int id)
        {
            var result = _commentService.RejectComment(id);
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
