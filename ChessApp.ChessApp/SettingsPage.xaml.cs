namespace ChessApp;

public partial class SettingsPage : ContentPage
{
    Settings Settings = new Settings();
	public SettingsPage()
	{

        
		InitializeComponent();

        // Set styles based on dark mode state
        if (Settings.Instance.DarkMode)
        {
            Resources["PageBackgroundColor"] = Resources["DarkPageBackgroundColor"];
            Resources["LightSwitchStyle"] = Resources["DarkSwitchStyle"];
        }
        else
        {
            Resources["PageBackgroundColor"] = Resources["LightPageBackgroundColor"];
            Resources["LightSwitchStyle"] = Resources["LightSwitchStyle"];
        }

        // Set each of the settings to their current selection
        Settings.Instance.Initialize();
        SoundEffects.IsToggled = Settings.Instance.SoundEffects;
        ConfirmMove.IsToggled = Settings.Instance.ConfirmMove;
        DarkMode.IsToggled = Settings.Instance.DarkMode;
        MoveHints.IsToggled = Settings.Instance.MoveHints;
        TurnTimer.IsToggled = Settings.Instance.TurnTimer;
    }

    // Reset settings button. Set all IsToggled values to true
	private void Click(object sender, EventArgs e)
	{
        SoundEffects.IsToggled = true;
        TurnTimer.IsToggled = true;
        DarkMode.IsToggled = false;
        ConfirmMove.IsToggled = true;
        MoveHints.IsToggled = true;
    }

    // Each of these functions handle when a switch is toggled
    private void SoundFXToggled(object sender, ToggledEventArgs e)
    {
        Settings.Instance.SoundEffects = e.Value;
    }

    private void TimerToggled(object sender, ToggledEventArgs e)
    {
        Settings.Instance.TurnTimer = e.Value;
    }

    private void DarkModeToggled(object sender, ToggledEventArgs e)
    {
        Settings.Instance.DarkMode = e.Value;
        // Update UI immediately based on the new Dark Mode state
        if (e.Value)
        {
            Resources["PageBackgroundColor"] = Resources["DarkPageBackgroundColor"];
            Resources["LightSwitchStyle"] = Resources["DarkSwitchStyle"];
        }
        else
        {
            Resources["PageBackgroundColor"] = Resources["LightPageBackgroundColor"];
            Resources["LightSwitchStyle"] = Resources["LightSwitchStyle"];
        }

        // Notify other pages that dark mode has been changed
        MessagingCenter.Send<SettingsPage, bool>(this, "DarkModeChanged", e.Value);

    }
    private void ConfirmToggled(object sender, ToggledEventArgs e)
    {
        Settings.Instance.ConfirmMove = e.Value;
    }

    private void HintsToggled(object sender, ToggledEventArgs e)
    {
        Settings.Instance.MoveHints = e.Value;
    }
}
