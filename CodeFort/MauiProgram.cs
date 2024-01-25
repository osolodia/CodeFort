using CodeFort.DataAccess;
using Microsoft.Extensions.Logging;

namespace CodeFort
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
            // Регистрация сервиса базы данных
            builder.Services.AddDbContext<ApplicationDbContext>();

            // Регистрация сервиса MainPage 
            builder.Services.AddTransient<MainPage>();

            // Создание временного контекста базы данных, чтобы выполнить миграции
            var dbContext = new ApplicationDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
