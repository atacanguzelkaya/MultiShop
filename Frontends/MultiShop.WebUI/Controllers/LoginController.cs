﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "Default");
        }
    }
}
