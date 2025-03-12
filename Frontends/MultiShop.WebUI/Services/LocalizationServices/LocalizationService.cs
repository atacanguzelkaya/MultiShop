using Microsoft.Extensions.Localization;
using MultiShop.WebUI.Resources;
using System.Reflection;

namespace MultiShop.WebUI.Services.LocalizationServices
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory localizerFactory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = localizerFactory.Create("SharedResource", assemblyName.Name!);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            var result = _localizer[key];
            return result;
        }
    }
}
