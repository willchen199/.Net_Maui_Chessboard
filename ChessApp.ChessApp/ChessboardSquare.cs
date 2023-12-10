using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChessApp.Chesspieces;
using ChessApp.Moving;

namespace ChessApp;

/// <summary>
/// Represents a single square on the chessboard.
/// </summary>
public class ChessboardSquare : INotifyPropertyChanged
{
    // Fields to store the properties of a chessboard square.
    private int _column;
    private int _height;
    private int _row;
    private int _width;
    private Color _color;
    private IChesspiece _chessPiece;

    /// <summary>
    /// Constructor for ChessboardSquare. Initializes a new instance with specified dimensions and position.
    /// </summary>
    /// <param name="width">Width of the square.</param>
    /// <param name="height">Height of the square.</param>
    /// <param name="row">Row number of the square on the chessboard.</param>
    /// <param name="column">Column number of the square on the chessboard.</param>
    /// <param name="chessPiece">The chess piece on this square</param>
    public ChessboardSquare(int width, int height, int row, int column, IChesspiece chessPiece)
    {
        _width = width;
        _height = height;
        _row = row;
        _column = column;
        _chessPiece = chessPiece;
    }
    
    public ChessboardSquare(){}

    public Position Position => new(Row, Column);

    /// <summary>
    /// Gets or sets the row of the chessboard square.
    /// </summary>
    public int Row
    {
        get => _row;
        set => SetField(ref _row, value);
    }

    /// <summary>
    /// Gets or sets the column of the chessboard square.
    /// </summary>
    public int Column
    {
        get => _column;
        set => SetField(ref _column, value);
    }

    /// <summary>
    /// Gets or sets the image source for the chessboard square.
    /// </summary>
    public IChesspiece Chesspiece
    {
        get => _chessPiece;
        set => SetField(ref _chessPiece, value);
    }

    /// <summary>
    /// Gets or sets the image source for the chessboard square.
    /// </summary>
    public Color Color
    {
        get => _color;
        set => SetField(ref _color, value);
    }

    /// <summary>
    /// Gets or sets the width of the chessboard square.
    /// </summary>
    public int Width
    {
        get => _width;
        set => SetField(ref _width, value);
    }

    /// <summary>
    /// Gets or sets the height of the chessboard square.
    /// </summary>
    public int Height
    {
        get => _height;
        set => SetField(ref _height, value);
    }

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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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