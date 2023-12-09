namespace ChessApp;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
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


}