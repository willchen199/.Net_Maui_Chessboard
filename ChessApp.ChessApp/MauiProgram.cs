// Importing necessary namespaces
using Microsoft.Extensions.Logging; // Logging utilities for application
using Orbit.Engine; // Orbit Engine library
using CommunityToolkit.Maui; // Community Toolkit for MAUI

// Namespace declaration for the ChessApp
namespace ChessApp
{
    // Static class representing the main program for MAUI
    public static class MauiProgram // Run on mac using `dotnet build -t:Run -f net7.0-maccatalyst`
    {
        // Method to create and configure the MAUI app
        public static MauiApp CreateMauiApp()
        {
            // Create a builder for the MAUI app
            var builder = MauiApp.CreateBuilder();

            // Configure the MAUI app to use the specified App class and fonts
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                // Add custom fonts to the app
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseOrbitEngine() // Use the Orbit Engine for additional features
              .UseMauiCommunityToolkit(); // Use the MAUI Community Toolkit for additional controls and utilities

#if DEBUG
            // Add debugging capabilities in DEBUG mode
            builder.Logging.AddDebug();
#endif

            // Build and return the configured MAUI app
            return builder.Build();
        }
    }
}
