using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChessApp.Chesspieces;

namespace ChessApp;

/// <summary>
/// Represents the view model for the chessboard in a chess game application. 
/// It encapsulates the presentation logic and data for the chessboard, handling
/// the creation of squares and managing the state of the game, including turn management.
/// </summary>
public class ChessboardVM : INotifyPropertyChanged
{
    /// <summary>
    /// Initializes a new instance of the ChessboardVM class. 
    /// It creates an 8x8 grid of ChessboardSquare objects each initialized with 
    /// the appropriate chess piece for the starting position of a chess game.
    /// </summary>
    public ChessboardVM()
    {
        Squares = new ObservableCollection<ChessboardSquare>(); // Initialize the collection of squares.

        for (int col = 0; col < 8; col++)
        for (int row = 0; row < 8; row++)
        {
            bool isWhiteSquare = (row + col) % 2 == 0;
            var squareColor = isWhiteSquare ? Colors.MintCream : Colors.SandyBrown;

            var chessPiece = CreateInitialPiece(row, col);

            Squares.Add(new ChessboardSquare(45, 45, row, col, chessPiece)
            {
                Chesspiece = chessPiece,
                Color = squareColor
            });
        }

        IsWhiteTurn = true;
    }

    /// <summary>
    /// Initializes a new instance of the ChessboardVM class with a given collection of squares.
    /// This constructor can be used when you need to set up a chessboard with a pre-defined state.
    /// </summary>
    /// <param name="squares">A collection of ChessboardSquare objects representing the chessboard layout.</param>
    public ChessboardVM(ObservableCollection<ChessboardSquare> squares)
    {
        Squares = squares;
    }

    /// <summary>
    /// Creates the initial chess piece for a given position on the chessboard.
    /// </summary>
    /// <param name="row">The row where the piece will be placed.</param>
    /// <param name="column">The column where the piece will be placed.</param>
    /// <returns>An instance of a chess piece that should occupy the given row and column at the start of the game.</returns>
    private IChesspiece CreateInitialPiece(int row, int column)
    {
        if (row == 1 || row == 6)
        {
            return new Pawn(row == 1 ? "pawn_b.png" : "pawn_w.png", row, column);
        }

        if (row == 0 || row == 7)
        {
            switch (column)
            {
                // Spawn Rooks
                case 0:
                case 7:
                    return new Rook(row == 0 ? "rook_b.png" : "rook_w.png", row, column);

                // Spawn Knights
                case 1:
                case 6:
                    return new Knight(row == 0 ? "knight_b.png" : "knight_w.png", row, column);

                // Spawn Bishops
                case 2:
                case 5:
                    return new Bishop(row == 0 ? "bishop_b.png" : "bishop_w.png", row, column);

                // Spawn Queens
                case 3:
                    return new Queen(row == 0 ? "queen_b.png" : "queen_w.png", row, column);

                // Spawn Kings
                case 4:
                    return new King(row == 0 ? "king_b.png" : "king_w.png", row, column);
            }
        }

        return new NoPiece("blank_square.png", row, column);
    }

    /// <summary>
    /// Moves a chess piece from one square to another on the chessboard.
    /// It updates the positions of the chess pieces within the internal collection
    /// and notifies the UI about the change to update the view.
    /// </summary>
    /// <param name="currentSquare">The square from which a chess piece is being moved.</param>
    /// <param name="newSquare">The square to which the chess piece is moving.</param>
    public void MovePiece(ChessboardSquare currentSquare, ChessboardSquare newSquare)
    {
        // Perform the move
        newSquare.Chesspiece = currentSquare.Chesspiece;
        currentSquare.Chesspiece = new NoPiece("blank_square.png", currentSquare.Row, currentSquare.Column);

        // Update the position of the piece
        newSquare.Chesspiece.CurrentRow = newSquare.Row;
        newSquare.Chesspiece.CurrentColumn = newSquare.Column;

        // Notify the UI to refresh the squares
        OnPropertyChanged(nameof(Squares));
    }


    /// <summary>
    /// An ObservableCollection of ChessboardSquare objects that represents the squares on the chessboard.
    /// </summary>
    public ObservableCollection<ChessboardSquare> Squares { get; } // Property to hold the squares.

    /// <summary>
    /// An event that is raised when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Invokes the PropertyChanged event to notify the UI that a property value has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName)); // Notify that a property has changed.
    }

    private bool isWhiteTurn;

    /// <summary>
    /// Gets or sets a value indicating whether it is White's turn.
    /// </summary>
    public bool IsWhiteTurn
    {
        get => isWhiteTurn;
        set
        {
            if (isWhiteTurn != value)
            {
                isWhiteTurn = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TurnText)); // Notify that TurnText needs to be updated
            }
        }
    }

    /// <summary>
    /// Gets the text representation of whose turn it is.
    /// </summary>
    public string TurnText => IsWhiteTurn ? "White Turn" : "Black Turn";


    /// <summary>
    /// Sets the field to the given value and triggers the PropertyChanged event if the value has changed.
    /// </summary>
    /// <typeparam name="T">The type of the field that is being changed.</typeparam>
    /// <param name="field">A reference to the field being changed.</param>
    /// <param name="value">The new value for the field.</param>
    /// <param name="propertyName">The name of the property (automatically obtained).</param>
    /// <returns>true if the field was changed; otherwise, false.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        // Check if the value is the same as the current value.
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value; // Set the new value.
        OnPropertyChanged(propertyName); // Notify that the property has changed.
        return true;
    }
}