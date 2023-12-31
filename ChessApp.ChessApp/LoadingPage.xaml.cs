using System.Collections.ObjectModel;
using ChessApp.DataBaseAccess;
using Newtonsoft.Json;

namespace ChessApp;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
        
        // Subscribe to messages related to dark mode changes
        SubscribeToMessages();

        // Set styles based on the current dark mode state
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

    // Subscribe to messages regarding dark mode changes
    private void SubscribeToMessages()
    {
        MessagingCenter.Subscribe<SettingsPage, bool>(this, "DarkModeChanged", OnDarkModeChanged);
    }
    
    // Event handler for dark mode changes
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

    // Update UI styles for dark mode
    private void UpdateDarkModeStyles()
    {
        Resources["PageBackgroundColor"] = Resources["DarkPageBackgroundColor"];
        Resources["LightSwitchStyle"] = Resources["DarkSwitchStyle"];
        Resources["LabelTextStyle"] = Resources["DarkLabelTextStyle"];
    }

    // Update UI styles for light mode
    private void UpdateLightModeStyles()
    {
        Resources["PageBackgroundColor"] = Resources["LightPageBackgroundColor"];
        Resources["LightSwitchStyle"] = Resources["LightSwitchStyle"];
        Resources["LabelTextStyle"] = Resources["LightLabelTextStyle"];
    }

    // Deserialize ChessboardVM from JSON
    private ChessboardVM DeserializeChessboardVM(string json)
    {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new ChesspieceJsonConverter());
        var chessboardVM = JsonConvert.DeserializeObject<ChessboardVM>(json, settings);
        return chessboardVM;
    }

    // Event handler for loading saved game from Slot 1
    private async void SavedSlot1(object sender, EventArgs e)
    {
        // Download saved game data from Azure Blob Storage
        string savedGame = await AccessAzureBlob.DownloadToStringAsync("testUser", "testBlobOne");
        try
        {
            // Deserialize the ChessboardVM from the downloaded JSON
            ChessboardVM chessboardVM = DeserializeChessboardVM(savedGame);
            chessboardVM.Squares = new ObservableCollection<ChessboardSquare>(chessboardVM.Squares.TakeLast(64));
            
            // If deserialization is successful, navigate to ChessPage with the loaded ChessboardVM
            if (chessboardVM != null)
                await Navigation.PushAsync(new ChessPage(chessboardVM));
        }
        catch (Exception exception)
        {
            // Handle and log exceptions that may occur during the process
            Console.WriteLine(exception);
            throw;
        }
    }
}