// Importing necessary namespaces
using Microsoft.Maui.Controls; // Namespace for MAUI controls

// Namespace declaration for the ChessApp
namespace ChessApp
{
    // Class representing the application settings
    public class Settings : BindableObject
    {
        // Singleton instance of the Settings class
        private static Settings _instance;

        // Property to get the singleton instance
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                    _instance.Initialize();
                }
                return _instance;
            }
        }

        // Data binding for Sound Effects
        public static readonly BindableProperty SoundEffectsProperty =
            BindableProperty.Create(
                nameof(SoundEffects),
                typeof(bool),
                typeof(Settings),
                true);

        // Property for Sound Effects setting
        public bool SoundEffects
        {
            get { return (bool)GetValue(SoundEffectsProperty); }
            set
            {
                // Set the property value
                SetValue(SoundEffectsProperty, value);

                // Save the value to Preferences for persistence
                Preferences.Set(nameof(SoundEffects), value);
            }
        }

        // Data binding for Move Hints
        public static readonly BindableProperty MoveHintsProperty =
            BindableProperty.Create(
                nameof(MoveHints),
                typeof(bool),
                typeof(Settings),
                true);

        // Property for Move Hints setting
        public bool MoveHints
        {
            get { return (bool)GetValue(MoveHintsProperty); }
            set
            {
                // Set the property value
                SetValue(MoveHintsProperty, value);

                // Save the value to Preferences for persistence
                Preferences.Set(nameof(MoveHints), value);
            }
        }

        // Data binding for Turn Timer
        public static readonly BindableProperty TurnTimerProperty =
            BindableProperty.Create(
                nameof(TurnTimer),
                typeof(bool),
                typeof(Settings),
                true);

        // Property for Turn Timer setting
        public bool TurnTimer
        {
            get { return (bool)GetValue(TurnTimerProperty); }
            set
            {
                // Set the property value
                SetValue(TurnTimerProperty, value);

                // Save the value to Preferences for persistence
                Preferences.Set(nameof(TurnTimer), value);
            }
        }

        // Data binding for Confirm Move
        public static readonly BindableProperty ConfirmMoveProperty =
            BindableProperty.Create(
                nameof(ConfirmMove),
                typeof(bool),
                typeof(Settings),
                true);

        // Property for Confirm Move setting
        public bool ConfirmMove
        {
            get { return (bool)GetValue(ConfirmMoveProperty); }
            set
            {
                // Set the property value
                SetValue(ConfirmMoveProperty, value);

                // Save the value to Preferences for persistence
                Preferences.Set(nameof(ConfirmMove), value);
            }
        }

        // Data binding for Dark Mode
        public static readonly BindableProperty DarkModeProperty =
            BindableProperty.Create(
                nameof(DarkMode),
                typeof(bool),
                typeof(Settings),
                true);

        // Property for Dark Mode setting
        public bool DarkMode
        {
            get { return (bool)GetValue(DarkModeProperty); }
            set
            {
                // Set the property value
                SetValue(DarkModeProperty, value);

                // Save the value to Preferences for persistence
                Preferences.Set(nameof(DarkMode), value);
            }
        }

        // Method to initialize settings with stored values
        public void Initialize()
        {
            // Initialize settings with stored values. Allows the user to
            // quit the app and return with the same settings set.
            SoundEffects = Preferences.Get(nameof(SoundEffects), true);
            MoveHints = Preferences.Get(nameof(MoveHints), true);
            ConfirmMove = Preferences.Get(nameof(ConfirmMove), true);
            DarkMode = Preferences.Get(nameof(DarkMode), true);
            TurnTimer = Preferences.Get(nameof(TurnTimer), true);
        }
    }
}
