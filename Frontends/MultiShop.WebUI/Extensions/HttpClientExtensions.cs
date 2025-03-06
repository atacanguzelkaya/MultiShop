using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Concretes;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.WebUI.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(values.IdentityServerUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICategoryService, CategoryService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IProductService, ProductService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IAboutService, AboutService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IBrandService, BrandService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IFeatureService, FeatureService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IContactService, ContactService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ICommentService, CommentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IMessageService, MessageService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
            {
                opt.BaseAddress = new Uri(values.IdentityServerUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        }
    }
}