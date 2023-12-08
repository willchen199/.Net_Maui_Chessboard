using Microsoft.Maui.Controls;

namespace ChessApp
{
    public class Settings : BindableObject
    {
        private static Settings _instance;

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

        public bool SoundEffects
        {
            get { return (bool)GetValue(SoundEffectsProperty); }
            set
            {
                SetValue(SoundEffectsProperty, value);
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

        public bool MoveHints
        {
            get { return (bool)GetValue(MoveHintsProperty); }
            set
            {
                SetValue(MoveHintsProperty, value);
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

        public bool TurnTimer
        {
            get { return (bool)GetValue(TurnTimerProperty); }
            set
            {
                SetValue(TurnTimerProperty, value);
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

        public bool ConfirmMove
        {
            get { return (bool)GetValue(ConfirmMoveProperty); }
            set
            {
                SetValue(ConfirmMoveProperty, value);
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

        public bool DarkMode
        {
            get { return (bool)GetValue(DarkModeProperty); }
            set
            {
                SetValue(DarkModeProperty, value);
                Preferences.Set(nameof(DarkMode), value);
            }
        }

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
