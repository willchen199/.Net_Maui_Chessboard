using ChessApp.Chesspieces;
using ChessApp.Moving;
using CommunityToolkit.Maui.Views;

namespace ChessApp;

/// <summary>
/// Represents the Chessboard page.
/// </summary>
public partial class ChessPage : ContentPage
{
    /// <summary>
    /// Flag to check if the page is loaded.
    /// </summary>
    private readonly bool isLoaded;

    private int height;
    private double smallestDimension;
    private int width;

    /// <summary>
    /// Constructor for ChessPage. Initializes the page components and sets up the view model.
    /// </summary>
    public ChessPage()
    {
        InitializeComponent(); // Initialize the page's components.
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
        BindingContext = new ChessboardVM(); // Set the binding context to a new Chessboard view model.
        isLoaded = true; // Flag to indicate the page is loaded.
        SelectedSquare = new ChessboardSquare(0, 0, 0, 0, new NoPiece("", 4, 4));
    }
    
    public ChessPage(ChessboardVM chessboardVM)
    {
        InitializeComponent(); // Initialize the page's components.
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
        BindingContext = chessboardVM; // Set the binding context to a new Chessboard view model.
        isLoaded = true; // Flag to indicate the page is loaded.
        SelectedSquare = new ChessboardSquare(0, 0, 0, 0, new NoPiece("", 4, 4));
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
    /// <summary>
    /// Width property of the ChessPage. Notifies when the property changes.
    /// </summary>
    public int Width
    {
        get => width;
        set
        {
            if (width != value)
            {
                width = value;
                OnPropertyChanged(); // Notify that Width has changed.
            }
        }
    }

    /// <summary>
    /// Height property of the ChessPage. Notifies when the property changes.
    /// </summary>
    public int Height
    {
        get => height;
        set
        {
            if (height != value)
            {
                height = value;
                OnPropertyChanged(); // Notify that Height has changed.
            }
        }
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

    /// <summary>
    /// The currently selected square on the chessboard.
    /// </summary>
    private ChessboardSquare SelectedSquare { get; set; }

    /// <summary>
    /// Event handler for when a chess square is clicked. Toggles the color of the chess square.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event data.</param>
    private void ChessSquare_OnClicked(object sender, EventArgs e)
    {
        if (sender is not ImageButton { BindingContext: ChessboardSquare clickedSquare })
            return;

        var chessboardVM = (ChessboardVM)BindingContext;

        if (CanSelectSquare(clickedSquare, chessboardVM))
            SelectedSquare = clickedSquare;
        
        // Highlight legal moves if a square is selected
        chessboardVM.HighlightLegalMoves(SelectedSquare);
    }
    
    /// <summary>
    /// Checks if the clicked square can be selected.
    /// </summary>
    /// <param name="clickedSquare">The target square.</param>
    /// <param name="chessboardVM">The chessboard's VM.</param>
    /// <returns></returns>
    private bool CanSelectSquare(ChessboardSquare clickedSquare, ChessboardVM chessboardVM)
    {
        // If the clicked square has no piece, and no square is currently selected, return false
        if (clickedSquare.Chesspiece.Name.Equals(ChesspieceName.None) && SelectedSquare == null)
            return false;

        // If the clicked square has a piece and it's the turn of that piece's color, select the square
        if (!clickedSquare.Chesspiece.Name.Equals(ChesspieceName.None) &&
            IsColorsTurn(clickedSquare.Chesspiece.Color, chessboardVM))
        {
            return true;
        }

        // If a square is already selected and the clicked square is a valid move target
        if (SelectedSquare != null && SelectedSquare.Chesspiece.CanMove(SelectedSquare, clickedSquare, chessboardVM.Squares))
        {
            MovePiece(SelectedSquare, clickedSquare, chessboardVM); // Perform the move
            chessboardVM.IsWhiteTurn = !chessboardVM.IsWhiteTurn; // Change the turn
            SelectedSquare = null; // Deselect the square
            return false; // No need to select the clicked square after moving
        }

        return false;
    }

    /// <summary>
    /// Checks if it is the turn of the specified square's piece color.
    /// </summary>
    /// <param name="pieceColor">The color of the chess piece.</param>
    /// <param name="chessboardVM">The chessboard's VM.</param>
    /// <returns></returns>
    private bool IsColorsTurn(Color pieceColor, ChessboardVM chessboardVM)
    {
        return (pieceColor.Equals(Colors.White) && chessboardVM.IsWhiteTurn) ||
               (!pieceColor.Equals(Colors.White) && !chessboardVM.IsWhiteTurn);
    }


    /// <summary>
    /// Moves a chess piece from one square to another on the chessboard.
    /// </summary>
    /// <param name="fromSquare">The square from which a chess piece is being moved.</param>
    /// <param name="toSquare">The square to which the chess piece is moving.</param>
    private void MovePiece(ChessboardSquare fromSquare, ChessboardSquare toSquare, ChessboardVM chessboardVM)
    {
        chessboardVM.MovePiece(fromSquare, toSquare);
    }

    /// <summary>
    /// Event handler for when the size of the ChessPage changes. Adjusts the chessboard size.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event data.</param>
    private void ChessPage_OnSizeChanged(object sender, EventArgs e)
    {
        // Return if the page is not loaded or sender is not a ContentPage.
        if (!isLoaded || sender is not ContentPage page)
            return;

        // Calculate the smallest dimension for the chessboard.
        smallestDimension = Math.Min(page.Width * 0.85, page.Height * 0.8);

        // Set the requested width and height for the chessboard view.
        UxChessboardView.WidthRequest = smallestDimension;
        UxChessboardView.HeightRequest = smallestDimension;
        AdjustChessboardPieces(); // Adjust the chessboard pieces based on the new size.
    }

    /// <summary>
    /// Adjusts the size of the chessboard pieces based on the current chessboard view size.
    /// </summary>
    private void AdjustChessboardPieces()
    {
        double size = UxChessboardView.Width / 8; // Calculate the size for each chessboard square.
        // Iterate over each chessboard square and set its size.
        foreach (var item in (IEnumerable<ChessboardSquare>)UxChessboardView.ItemsSource)
        {
            item.Width = (int)size;
            item.Height = (int)size;
        }
    }

    /// <summary>
    /// Event handler for when the size of the chessboard view changes. Adjusts the chessboard pieces.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event data.</param>
    private void ChessboardView_OnSizeChanged(object sender, EventArgs e)
    {
        // Check if the sender is a CollectionView and the page is loaded.
        if (sender is CollectionView collectionView && isLoaded)
            AdjustChessboardPieces(); // Adjust the chessboard pieces.
    }

    private void OpenPausePage(object sender, EventArgs e)
    {
        var chessboardVM = (ChessboardVM)BindingContext;
        var pausePopup = new PausePopup(chessboardVM);
        this.ShowPopupAsync(pausePopup);
    }
}