using Microsoft.Extensions.Logging;
using Orbit.Engine;

namespace ChessApp;

public static class MauiProgram // Run on mac using `dotnet build -t:Run -f net7.0-maccatalyst`
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
            })
            .UseOrbitEngine();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}