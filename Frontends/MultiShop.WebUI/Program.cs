using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Resources;
using MultiShop.WebUI.Services.LocalizationServices;
using MultiShop.WebUI.Settings;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationServices();
builder.Services.AddScopedServices();
builder.Services.AddHttpClients(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

// Globalization and MultiLanguge
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(opt =>
    {
        opt.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assembly = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResource", assembly.Name);
        };
    });
//builder.Services.Configure<RequestLocalizationOptions>(opt =>
//{
//    var cultures = new List<CultureInfo>
//    {
//    new CultureInfo("tr"),
//    new CultureInfo("en")
//    };

//    opt.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr"));
//    opt.SupportedCultures = cultures;
//    opt.SupportedUICultures = cultures;

//    opt.RequestCultureProviders = new List<IRequestCultureProvider>()
//    {
//    new QueryStringRequestCultureProvider(),
//    new CookieRequestCultureProvider(),
//    new AcceptLanguageHeaderRequestCultureProvider()
//    };
//});
builder.Services.AddScoped<LocalizationService>();

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var cultures = new List<CultureInfo>
    {
        new CultureInfo("tr"),
        new CultureInfo("en")
    };

    opt.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr"));
    opt.SupportedCultures = cultures;
    opt.SupportedUICultures = cultures;

    opt.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});



var app = builder.Build();
app.UseRequestLocalization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}"
    );
});

app.Run();