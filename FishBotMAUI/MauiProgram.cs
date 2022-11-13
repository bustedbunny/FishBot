using FishBotMAUI.Pages;
using FishBotMAUI.ViewModel;

namespace FishBotMAUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var services = builder.Services;

            services.AddSingleton<MainPage>();
            services.AddSingleton<MainPageViewModel>();

            services.AddSingleton<FishPage>();
            services.AddSingleton<FishPageViewModel>();

            return builder.Build();
        }
    }
}