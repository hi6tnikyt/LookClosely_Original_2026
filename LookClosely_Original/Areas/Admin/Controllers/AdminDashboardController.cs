using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LookClosely_Original.Areas.Admin.Controllers
{
    [Area("Admin")] 
    [Authorize(Roles = "Admin")] 
    public class AdminDashboardController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
