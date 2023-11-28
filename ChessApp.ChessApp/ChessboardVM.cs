using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChessApp.Chesspieces;

namespace ChessApp;

/// <summary>
/// Represents the view model for the chessboard. It handles the logic for creating and managing chessboard squares.
/// </summary>
public class ChessboardVM : INotifyPropertyChanged
{
    /// <summary>
    /// Constructor for ChessboardVM. Initializes the chessboard with squares.
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
    }

    public ChessboardVM(ObservableCollection<ChessboardSquare> squares)
    {
        Squares = squares;
    }

    /// <summary>
    /// Create the initial chess piece.
    /// </summary>
    /// <param name="row">The row this piece will spawn in.</param>
    /// <param name="column">What column is this in?</param>
    /// <returns></returns>
    private IChesspiece CreateInitialPiece(int row, int column)
    {
        if(row == 1 || row == 6)
        {
            return new Pawn(row == 1 ? "pawn_b.png" : "pawn_w.png", row, column);
        }

        if(row == 0 || row == 7)
        {
            switch (column)
            {
                case 3:
                    return new Queen(row == 0 ? "queen_b.png" : "queen_w.png", row, column);
                case 4:
                    return new King(row == 0 ? "king_b.png" : "king_w.png", row, column);
                case 5:
                    return new Bishop(row == 0 ? "bishop_b.png" : "bishop_w.png", row, column);
                case 6:
                    return new Knight(row == 0 ? "knight_b.png" : "knight_w.png", row, column);
                case 7:
                    return new Rook(row == 0 ? "rook_b.png" : "rook_w.png", row, column);
            }
        }

        return new NoPiece("blank_square.png", row, column);
    }
    
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
    /// Collection of chessboard squares. Represents each square on the chessboard.
    /// </summary>
    public ObservableCollection<ChessboardSquare> Squares { get; } // Property to hold the squares.

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Method to invoke the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Notify that a property has changed.
    }

    /// <summary>
    /// Helper method to set the field value and trigger the PropertyChanged event.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="field">The field to be set.</param>
    /// <param name="value">The new value for the field.</param>
    /// <param name="propertyName">The name of the property (automatically obtained).</param>
    /// <returns>True if the field was changed, false otherwise.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        // Check if the value is the same as the current value.
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value; // Set the new value.
        OnPropertyChanged(propertyName); // Notify that the property has changed.
        return true;
    }
}
