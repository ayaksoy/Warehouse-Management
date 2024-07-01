using EKStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EKStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =OtherRoles.Role_Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
