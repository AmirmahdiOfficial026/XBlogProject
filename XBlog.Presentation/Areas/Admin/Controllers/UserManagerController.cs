using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XBlog.Infrastructure.Identity;
using XBlog.Presentation.Areas.Admin.Models;

namespace XBlog.Presentation.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public List<string> Roles { get; set; }
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult UsersList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult> Index(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            UserRoleModelView model = new UserRoleModelView()
            {
                CurrentUser = currentUser,
                UserRoles = (await _userManager.GetRolesAsync(currentUser)).ToList(),
                AllRoles = _roleManager.Roles.ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SelectRole(string id)
        {
            UserRoleModelView model = new UserRoleModelView()
            {
                CurrentUser = await _userManager.FindByIdAsync(id),
                AllRoles = _roleManager.Roles.ToList(),
            };
            foreach (var role in model.AllRoles)
            {
                if (Roles.Contains(role.Name))
                {
                    if (!(await _userManager.IsInRoleAsync(model.CurrentUser, role.Name)))
                    {
                        await _userManager.AddToRoleAsync(model.CurrentUser, role.Name);
                    }
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(model.CurrentUser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(model.CurrentUser, role.Name);
                    }
                }
            }
            TempData["Success"] = "User updating successfully";
            return RedirectToAction("UsersList");
        }
    }
}
