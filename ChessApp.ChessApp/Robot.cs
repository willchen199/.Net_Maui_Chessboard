using System.ComponentModel;

namespace ChessApp;

public class Robot : INotifyPropertyChanged
{
    private float _positionX;

    private float _positionY;

    public Robot(string imageFileName)
    {
        ImageFileName = imageFileName;
    }

    public string ImageFileName { get; }

    public float PositionX
    {
        get => _positionX;
        set
        {
            if (_positionX != value)
            {
                _positionX = value;
                OnPropertyChanged(nameof(PositionX));
            }
        }
    }

    public float PositionY
    {
        get => _positionY;
        set
        {
            if (_positionY != value)
            {
                _positionY = value;
                OnPropertyChanged(nameof(PositionY));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}