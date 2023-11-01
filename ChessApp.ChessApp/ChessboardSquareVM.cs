using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessApp;

public class ChessboardSquareVM : INotifyPropertyChanged
{
    private readonly ChessboardSquareModel square;

    public ChessboardSquareVM(ChessboardSquareModel square)
    {
        this.square = square;
    }

    public int OriginalRow => square.Row;
    public int OriginalCol => square.Col;
    
    public string BackgroundImageSource => square.Piece?.BackgroundPieceImage;
    public string ImageSource => square.Piece?.Image;

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