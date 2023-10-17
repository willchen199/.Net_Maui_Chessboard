using System.ComponentModel;

namespace ChessApp;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private int _count;

    private bool _imageSelected;
    private Robot _robot1;

    public MainPage()
    {
        Robot1 = new Robot("dotnet_bot.png");
        InitializeComponent();
        BindingContext = this;
    }

    public Robot Robot1
    {
        get => _robot1;
        set
        {
            if (_robot1 != value)
            {
                _robot1 = value;
                OnPropertyChanged(nameof(Robot1));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;

        if (_count == 1)
            CounterBtn.Text = $"Clicked {_count} time";
        else
            CounterBtn.Text = $"Clicked {_count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnImageTapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        if (!_imageSelected) _imageSelected = true;
        // Robot1.PositionX = e.GetPosition();
    }

    private void OnScrollViewTapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        Point? relativeToContainerPosition = e.GetPosition((View)sender);
        if (relativeToContainerPosition != null)
        {
            Console.WriteLine(relativeToContainerPosition.Value.X);
            Console.WriteLine(relativeToContainerPosition.Value.Y);
        }

        Console.WriteLine("Not image");
    }
}