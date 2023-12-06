using ChessApp.Chesspieces;

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
        BindingContext = new ChessboardVM(); // Set the binding context to a new Chessboard view model.
        isLoaded = true; // Flag to indicate the page is loaded.
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

        if (SelectedSquare == null &&
            ((chessboardVM.IsWhiteTurn && !Equals(clickedSquare.Chesspiece.Color, Colors.White)) ||
             (!chessboardVM.IsWhiteTurn && !Equals(clickedSquare.Chesspiece.Color, Colors.Black))))
        {
            return; // It's not the turn of the piece's color, so return early
        }

        // If a square is already selected and the clicked square is different
        if (SelectedSquare != null && clickedSquare != SelectedSquare)
        {
            // Check if the move is valid
            if (SelectedSquare.Chesspiece.CanMove(SelectedSquare, clickedSquare) &&
                SelectedSquare.Chesspiece.HasClearPath(SelectedSquare, clickedSquare, chessboardVM.Squares.ToList()))
            {
                MovePiece(SelectedSquare, clickedSquare);
                SelectedSquare.Chesspiece.Name = ChesspieceName.None;
                chessboardVM.IsWhiteTurn =
                    !chessboardVM.IsWhiteTurn; // Assuming you have a turn-based logic implemented
                SelectedSquare = null; // Reset the selected square
            }
            else
            {
                // Allow changing the selected square if the move is not valid
                if (clickedSquare.Chesspiece.Name != ChesspieceName.None)
                {
                    SelectedSquare = clickedSquare;
                }
            }
        }
        else if (SelectedSquare == null &&
                 clickedSquare.Chesspiece.Name != ChesspieceName.None &&
                 clickedSquare.Chesspiece != null)
        {
            // Select the square if it contains a piece
            SelectedSquare = clickedSquare;
        }

        //Highlighting legal piece movements
        chessboardVM.HighlightLegalMoves(SelectedSquare);

    }

    /// <summary>
    /// Moves a chess piece from one square to another on the chessboard.
    /// </summary>
    /// <param name="fromSquare">The square from which a chess piece is being moved.</param>
    /// <param name="toSquare">The square to which the chess piece is moving.</param>
    private void MovePiece(ChessboardSquare fromSquare, ChessboardSquare toSquare)
    {
        var chessboardVM = (ChessboardVM)BindingContext;
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
}