using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [AllowAnonymous]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            CategoryViewbagList();
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync("https://localhost:7070/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }          
            return View();
        }
        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            CategoryViewbagList();
            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var clint = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await clint.PostAsync("https://localhost:7070/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();     
        }

        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.DeleteAsync("https://localhost:7070/api/Categories?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            CategoryViewbagList();
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync("https://localhost:7070/api/Categories/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto> (jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var clint = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await clint.PutAsync("https://localhost:7070/api/Categories/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        void CategoryViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            ViewBag.v0 = "Kategori İşlemleri";
        }
    }
}