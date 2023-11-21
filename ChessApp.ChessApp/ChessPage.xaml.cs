﻿namespace ChessApp;

/// <summary>
/// Represents the main page of the Chess application.
/// </summary>
public partial class ChessPage : ContentPage
{
    private readonly bool isLoaded; // Flag to check if the page is loaded.

    private int height;

    private double smallestDimension; // Stores the smallest dimension of the chessboard.
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
    
    private ChessboardSquare selectedSquare = null;

    /// <summary>
    /// Event handler for when a chess square is clicked. Toggles the color of the chess square.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event data.</param>
    private void ChessSquare_OnClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is ChessboardSquare clickedSquare)
        {
            if (selectedSquare == null)
            {
                // Select the square if it has a piece
                if (clickedSquare.Chesspiece != null)
                    selectedSquare = clickedSquare;
            }
            else
            {
                // Move the piece if another square is clicked
                MovePiece(selectedSquare, clickedSquare);
                selectedSquare = null; // Deselect the piece after moving
            }
        }
    }
    
    private void MovePiece(ChessboardSquare fromSquare, ChessboardSquare toSquare)
    {
        if (fromSquare != null && toSquare != null)
        {
            var chessboardVM = (ChessboardVM)BindingContext;
            chessboardVM.MovePiece(fromSquare, toSquare);
        }
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
        if (sender is CollectionView collectionView && isLoaded) AdjustChessboardPieces(); // Adjust the chessboard pieces.
    }
}