using ChessApp.DataBaseAccess;
using Newtonsoft.Json;

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

    public ChessboardVM DeserializeChessboardVM(string json)
    {
        return JsonConvert.DeserializeObject<ChessboardVM>(json);
    }

    private async void SavedSlot1(object sender, EventArgs e)
    {
        string savedGame = await AccessAzureBlob.DownloadToStringAsync("testUser", "testBlobOne");
        try
        {
            ChessboardVM chessboardVM = DeserializeChessboardVM(savedGame);
            if (chessboardVM != null)
                await Navigation.PushAsync(new ChessPage(chessboardVM));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}