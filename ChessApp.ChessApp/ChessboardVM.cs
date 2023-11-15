using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        // Loop to create 8x8 squares for the chessboard.
        for (int row = 0; row < 8; row++)
        for (int col = 0; col < 8; col++)
        {
            bool isWhiteSquare = (row + col) % 2 == 0; // Determine if the square is white.
            var squareColor = isWhiteSquare ? Colors.White : Colors.Black; // Set the color of the square.

            // Add a new square to the collection with the specified properties.
            Squares.Add(new ChessboardSquare(45, 45, row, col)
            {
                ImageSource = ImageSource.FromFile("dotnet_bot.png"), // Set an image for the square.
                Color = squareColor // Set the color of the square.
            });
        }
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
