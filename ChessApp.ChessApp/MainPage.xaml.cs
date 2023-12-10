// Importing necessary namespaces
using ChessApp;

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

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MessagingCenter.Unsubscribe<PausePopup>(this, "NavigateToMainPage");
        MessagingCenter.Unsubscribe<PausePopup, ChessboardVM>(this, "NavigateToChessPage");
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

        // Event handler to open a new ChessPage
        private async void OpenNewChessPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChessPage());
        }

        // Event handler to open the SettingsPage
        private async void OpenSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        // Event handler to open the LoadingPage
        private async void OpenLoadingPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoadingPage());
        }

        /// <summary>
        /// Called when the page appears on the screen.
        /// Sets the minimum size for the window.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing(); // Call the base class implementation.

            // Set the minimum height and width for the window.
            Window.MinimumHeight = 850;
            Window.MinimumWidth = 750;
        }

        // Event handler for the "Load" button click
        private void OnLoadClick(object sender, EventArgs e)
        {
            // Uncomment the following line when implementing the functionality
            /// throw new NotImplementedException();
        }
    }
}
