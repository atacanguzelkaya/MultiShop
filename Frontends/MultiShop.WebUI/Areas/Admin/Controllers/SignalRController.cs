using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SignalRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
