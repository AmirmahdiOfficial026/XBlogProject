using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XBlog.Infrastructure.Identity;
using XBlog.Presentation.Models;

namespace XBlog.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email Not Found");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);
            if (result.Succeeded)
            {
                TempData["Success"] = "Welcome Back";
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is InValid");
                return View();
            }
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var resultLogin = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (resultLogin.Succeeded)
                {
                    TempData["Success"] = "Welcome";
                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is InValid");
                    return View(model);
                }
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            var model = await _userManager.GetUserAsync(User);

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = new UpdateUserAccountModelView
            {
                Id = model.Id,
                Username = model.UserName,
                Email = model.Email,
            };
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserAccountModelView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.UserName = model.Username;
            if (user == null) return View(model);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    result = await _userManager.AddPasswordAsync(user, model.Password);
                }
                TempData["Success"] = "User updating successfully";
                return View(model);
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
        }
    }
}
