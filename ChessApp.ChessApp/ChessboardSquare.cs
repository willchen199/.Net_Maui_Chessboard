using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChessApp;

public class ChessboardSquare : INotifyPropertyChanged
{
    public ChessboardSquare(int width, int height)
    {
        _width = width;
        _height = height;
    }
    
    private ImageSource imageSource;
    public ImageSource ImageSource
    {
        get => imageSource;
        set => SetField(ref imageSource, value);
    }

    private Color color;
    public Color Color
    {
        get => color;
        set => SetField(ref color, value);
    }

    private int _width;
    public int Width
    {
        get => _width;
        set => SetField(ref _width, value);
    }

    private int _height;
    public int Height
    {
        get => _height;
        set => SetField(ref _height, value);
    }

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