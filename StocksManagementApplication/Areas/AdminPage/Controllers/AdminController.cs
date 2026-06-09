using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StocksManagementApplication.Core.Enums;

namespace StocksManagementApplication.UI.Areas.AdminPage.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [Area("AdminPage")]
    public class AdminController : Controller
    {
        
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
