using Microsoft.AspNetCore.Mvc;

namespace PresentAte.Areas.Admin.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
