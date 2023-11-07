using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessApp;

public class ChessboardVM : INotifyPropertyChanged
{
    public ChessboardVM()
    {
        Squares = new ObservableCollection<ChessboardSquare>();

        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
        {
            var isWhiteSquare = (row + col) % 2 == 0;
            var squareColor = isWhiteSquare ? Colors.White : Colors.Black;

            Squares.Add(new ChessboardSquare(45, 45)
            {
                ImageSource = ImageSource.FromFile("white_tile.png"),
                Color = squareColor
            });
        }
    }

    public ObservableCollection<ChessboardSquare> Squares { get; private set; }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}