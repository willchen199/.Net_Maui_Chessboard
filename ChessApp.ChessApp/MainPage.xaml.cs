namespace ChessApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        SubscribeToMessages();
        // Set styles based on dark mode state
        Settings Settings = new Settings();
        if (Settings.Instance.DarkMode)
        {
            UpdateDarkModeStyles();
        }
        else
        {
            UpdateLightModeStyles();
        }
    }

    // Messages lets the program know if dark mode is toggled while the program is running. 
    private void SubscribeToMessages()
    {
        MessagingCenter.Subscribe<SettingsPage, bool>(this, "DarkModeChanged", OnDarkModeChanged);
    }


    private void OnDarkModeChanged(SettingsPage sender, bool isDarkMode)
    {
        // Update UI based on the new dark mode state
        if (isDarkMode)
        {
            UpdateDarkModeStyles();
        }
        else
        {
            UpdateLightModeStyles();
        }
    }

    private void UpdateDarkModeStyles()
    {
        Resources["PageBackgroundColor"] = Resources["DarkPageBackgroundColor"];
        Resources["LightSwitchStyle"] = Resources["DarkSwitchStyle"];
        Resources["LabelTextStyle"] = Resources["DarkLabelTextStyle"];
    }

    private void UpdateLightModeStyles()
    {
        Resources["PageBackgroundColor"] = Resources["LightPageBackgroundColor"];
        Resources["LightSwitchStyle"] = Resources["LightSwitchStyle"];
        Resources["LabelTextStyle"] = Resources["LightLabelTextStyle"];
    }

    private async void OnCounterClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChessPage());
    }

    private async void OpenSettings(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    private async void OpenLoadingPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoadingPage());
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
     ///    throw new NotImplementedException();
     }
}