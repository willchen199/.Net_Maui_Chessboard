using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessApp;

public class ChessboardSquareVM : INotifyPropertyChanged
{
    private readonly ChessboardSquareModel _square;

    public ChessboardSquareVM(ChessboardSquareModel square)
    {
        _square = square;
    }

    public string BackgroundImageSource => _square.Piece?.BackgroundPieceImage;
    public string ImageSource => _square.Piece?.Image;

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