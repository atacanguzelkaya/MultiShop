using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        void ProductImageViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
            ViewBag.v0 = "Ürün Görsel İşlemleri";
        }

        [Route("ProductImageDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ProductImageViewbagList();
            var values = await _productImageService.GetByProductIdProductImageAsync(id);
            return View(values);
        }

        [Route("ProductImageDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto updateProductImageDto, CreateProductImageDto createProductImageDto, string id)
        {
            updateProductImageDto.ProductId = id;
            createProductImageDto.ProductId = id;
            if (!ModelState.IsValid || string.IsNullOrEmpty(updateProductImageDto.ProductId))
            {
                await _productImageService.CreateProductImageAsync(createProductImageDto);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}