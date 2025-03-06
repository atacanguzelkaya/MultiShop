using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        public _ProductListComponentPartial(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id = null)
        {
            IEnumerable<ResultProductWithCategoryDto> values;

            if (string.IsNullOrEmpty(id))
            {
                values = await _productService.GetProductsWithCategoryAsync();
            }
            else
            {
                values = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
            }

            return View(values);
        }
    }
}