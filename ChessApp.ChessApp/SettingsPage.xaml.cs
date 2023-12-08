namespace ChessApp;

public partial class SettingsPage : ContentPage
{
    Settings Settings = new Settings();
	public SettingsPage()
	{
        // Set each of the settings to their current selection
		InitializeComponent();
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
