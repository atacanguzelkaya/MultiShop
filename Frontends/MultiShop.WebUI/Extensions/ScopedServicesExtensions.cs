using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Concretes;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Extensions
{
    public static class ScopedServicesExtensions
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            services.AddHttpClient();
        }
    }
}