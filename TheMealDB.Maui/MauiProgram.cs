using Microsoft.Extensions.Logging;
using Serilog;
using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Services;

namespace TheMealDB.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            

            var builder = MauiApp.CreateBuilder();
            SetupSerilog();
            builder.Services.AddSingleton<IMealService, MealService>();
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

            return builder.Build();
        }
        private static void SetupSerilog()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "debugcode.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path)
                .CreateLogger();
        }
    }
}