using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.ProductDetailDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ProductDetailViewbagList();
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync("https://localhost:7070/api/ProductDetails/GetProductDetailByProductId/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDetailDto> (jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateProductDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto, CreateProductDetailDto createProductDetailDto, string id)
        {
            updateProductDetailDto.ProductId = id;
            createProductDetailDto.ProductId = id;
            if (!ModelState.IsValid || string.IsNullOrEmpty(updateProductDetailDto.ProductId))
            {
                var clientCreate = _httpClientFactory.CreateClient();
                var jsonDataCreate = JsonConvert.SerializeObject(createProductDetailDto);
                StringContent stringContentCreate = new StringContent(jsonDataCreate, Encoding.UTF8, "application/json");
                var responseMessageCreate = await clientCreate.PostAsync("https://localhost:7070/api/ProductDetails", stringContentCreate);
                if (responseMessageCreate.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                }
                return View();
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7070/api/ProductDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }

        void ProductDetailViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Açıklama ve Bilgi Güncelleme Sayfası";
            ViewBag.v0 = "Ürün İşlemleri";
        }
    }
}