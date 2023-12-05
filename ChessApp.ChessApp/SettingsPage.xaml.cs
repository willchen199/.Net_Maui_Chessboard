namespace ChessApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	private void Click(object sender, EventArgs e)
	{
        SoundEffects.IsToggled = true;
        TurnTimer.IsToggled = true;
        DarkMode.IsToggled = true;
        ConfirmMove.IsToggled = true;
        MoveHints.IsToggled = true;
    }

    private void SoundFXToggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
		if (isOn)
		{
            // Switch is in the "On" state

            SoundEffects.ThumbColor = Color.FromArgb("#03C400"); // Set the OnColor when switch is On
        }
		else
		{
            // Switch is in the "Off" state
            SoundEffects.ThumbColor = Color.FromArgb("#FF0000"); // Set the OnColor when switch is Off
        }
    }

    private void TimerToggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
        if (isOn)
        {
            // Switch is in the "On" state

            TurnTimer.ThumbColor = Color.FromArgb("#03C400"); // Set the OnColor when switch is On
        }
        else
        {
            // Switch is in the "Off" state
            TurnTimer.ThumbColor = Color.FromArgb("#FF0000"); // Set the OnColor when switch is Off
        }
    }

    private void DarkModeToggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
        if (isOn)
        {
            // Switch is in the "On" state

            DarkMode.ThumbColor = Color.FromArgb("#03C400"); // Set the OnColor when switch is On
        }
        else
        {
            // Switch is in the "Off" state
            DarkMode.ThumbColor = Color.FromArgb("#FF0000"); // Set the OnColor when switch is Off
        }
    }
    private void ConfirmToggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
        if (isOn)
        {
            // Switch is in the "On" state

            ConfirmMove.ThumbColor = Color.FromArgb("#03C400"); // Set the OnColor when switch is On
        }
        else
        {
            // Switch is in the "Off" state
            ConfirmMove.ThumbColor = Color.FromArgb("#FF0000"); // Set the OnColor when switch is Off
        }
    }

    private void HintsToggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
        if (isOn)
        {
            // Switch is in the "On" state

            MoveHints.ThumbColor = Color.FromArgb("#03C400"); // Set the OnColor when switch is On
        }
        else
        {
            // Switch is in the "Off" state
            MoveHints.ThumbColor = Color.FromArgb("#FF0000"); // Set the OnColor when switch is Off
        }
    }
}
