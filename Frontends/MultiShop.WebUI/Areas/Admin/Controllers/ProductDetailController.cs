using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        void ProductDetailViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Açıklama ve Bilgi Güncelleme Sayfası";
            ViewBag.v0 = "Ürün İşlemleri";
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ProductDetailViewbagList();
            var values = await _productDetailService.GetByProductIdProductDetailAsync(id);
            return View(values);
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto, CreateProductDetailDto createProductDetailDto, string id)
        {
            updateProductDetailDto.ProductId = id;
            createProductDetailDto.ProductId = id;

            if (!ModelState.IsValid || string.IsNullOrEmpty(updateProductDetailDto.ProductId))
            {
                await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}