using Microsoft.Extensions.Logging;

namespace ReceiveApplicationVisualStudio
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
#if ANDROID
            builder.Services.AddTransient<IServiceFG, DemoServices>();
#endif
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDependencies();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ReceiveApplicationVisualStudio.MainPage>();
        }
    }
}
