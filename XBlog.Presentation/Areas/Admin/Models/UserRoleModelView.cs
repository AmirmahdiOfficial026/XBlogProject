using Microsoft.AspNetCore.Identity;
using XBlog.Infrastructure.Identity;

namespace XBlog.Presentation.Areas.Admin.Models;
public class UserRoleModelView
{
    public ApplicationUser CurrentUser { get; set; }
    public List<IdentityRole> AllRoles { get; set; }
    public List<string> UserRoles { get; set; }
}
