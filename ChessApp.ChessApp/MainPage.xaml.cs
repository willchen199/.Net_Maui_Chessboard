namespace ChessApp;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
		InitializeComponent();
	}

    private async void OnCounterClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChessPage());
    }

    private async void OpenSettings(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
    
    /// <summary>
    /// Called when the page appears on the screen. Sets the minimum size for the window.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing(); // Call the base class implementation.
        Window.MinimumHeight = 850; // Set the minimum height for the window.
        Window.MinimumWidth = 750; // Set the minimum width for the window.
    }

    private void OnLoadClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
