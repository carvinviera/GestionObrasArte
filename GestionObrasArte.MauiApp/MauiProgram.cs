using GestionObrasArte.MauiApp.Services;
using GestionObrasArte.MauiApp.ViewModels;
using GestionObrasArte.MauiApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace GestionObrasArte.MauiApp
{
    public static class MauiProgram
    {
        public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
        {
            var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Registrar Servicios (Singleton para el ApiService)
            builder.Services.AddSingleton<PinturasApiService>();

            // Registrar ViewModels (Transient o Scoped)
            builder.Services.AddTransient<PinturasListViewModel>();
            builder.Services.AddTransient<PinturaDetailViewModel>();

            // Registrar Vistas (Páginas)
            builder.Services.AddTransient<PinturasListPage>();
            builder.Services.AddTransient<PinturaDetailPage>();


            return builder.Build();
        }
    }
}