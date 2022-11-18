using FishBot;
using FishBotMAUI.Pages;
using FishBotMAUI.Services.FishBot;
using FishBotMAUI.Services.Logging;
using FishBotMAUI.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.CompilerServices;

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

            services.AddSingleton<Logger>();
            services.AddSingleton<FishingDatabase>();
            services.AddSingleton<CredentialSettings>();
            services.AddSingleton<FishBotController>();


            services.AddSingleton<StatusPageViewModel>();
            services.AddSingleton<StatusPage>();

            services.AddSingleton<SettingsPageViewModel>();
            services.AddSingleton<SettingsPage>();

            services.AddSingleton<FishPageViewModel>();
            services.AddSingleton<FishPage>();

            return builder.Build();
        }
    }
}