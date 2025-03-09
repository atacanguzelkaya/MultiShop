using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Dtos.IdentityDtos.RegisterDtos;
using MultiShop.WebUI.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
        }

        void LoginViewBag()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Giriş Sayfası";
            ViewBag.directory3 = "";
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoginViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            bool result = await _identityService.SignIn(signInDto);
            if (result)
            {
                return RedirectToAction("Index", "Default");
            }
            else
            {
                ModelState.AddModelError("", "Hatalı giriş, lütfen tekrar deneyin.");
                LoginViewBag();
                return View();
            }
        }
    }
}
