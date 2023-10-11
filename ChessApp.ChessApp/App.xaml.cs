using Microsoft.Maui.Controls;

namespace ChessApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}