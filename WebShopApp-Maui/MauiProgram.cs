using Microsoft.Extensions.Logging;
using WebShopApp_Maui.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebShopApp_Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            // Register HttpClient and the service without setting BaseAddress
            builder.Services.AddHttpClient<IWebShopAppService, WebShopAppService>();

            return builder.Build();
        }
    }
}
