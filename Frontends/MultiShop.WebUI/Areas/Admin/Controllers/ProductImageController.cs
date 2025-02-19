using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [AllowAnonymous]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ProductImageViewbagList();
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync("https://localhost:7070/api/ProductImages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData);
                return View(values);
            }          
            return View();
        }
        [HttpGet]
        [Route("CreateProductImage")]
        public IActionResult CreateProductImage()
        {
            ProductImageViewbagList();
            return View();
        }
        [HttpPost]
        [Route("CreateProductImage")]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            var clint = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductImageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await clint.PostAsync("https://localhost:7070/api/ProductImages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductImage", new { area = "Admin" });
            }
            return View();     
        }

        [Route("DeleteProductImage/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.DeleteAsync("https://localhost:7070/api/ProductImages?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductImage", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            ProductImageViewbagList();
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync("https://localhost:7070/api/ProductImages/ProductImagesByProductId?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductImageDto> (jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateProductImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            var clint = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductImageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await clint.PutAsync("https://localhost:7070/api/ProductImages/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductImage", new { area = "Admin" });
            }
            return View();
        }

        void ProductImageViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
            ViewBag.v0 = "Ürün Görsel İşlemleri";
        }
    }
}