using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MultiShop.WebUI.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = "/Login/Index/";
                    opt.LogoutPath = "/Login/LogOut/";
                    opt.AccessDeniedPath = "/Pages/AccessDenied/";
                    opt.Cookie.HttpOnly = false;
                    opt.Cookie.SameSite = SameSiteMode.Strict;
                    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    opt.Cookie.Name = "MultiShopJwt";
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = "/Login/Index/";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
                    opt.Cookie.Name = "MultiShopCookie";
                    opt.SlidingExpiration = false;
                });
        }
    }
}
