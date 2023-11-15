using System.Collections;

namespace ChessApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ChessboardVM();
        isLoaded = true;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Window.MinimumHeight = 850;
        Window.MinimumWidth = 750;
    }

    private void ChessSquare_OnClicked(object sender, EventArgs e)
    {
        if (sender.GetType() == typeof(ImageButton))
        {
            ImageButton pressedButton = (ImageButton)sender;
            if (pressedButton.Source == ImageSource.FromFile("black_tile.png"))
            {
                ImageSource.FromFile("white_tile.png");
            }
            else
            {
                pressedButton.Source = ImageSource.FromFile("black_tile.png");
            }
            
        }
    }
    
    private readonly bool isLoaded;
    
    private void MainPage_OnSizeChanged(object sender, EventArgs e)
    {
        if (!isLoaded || sender is not ContentPage page)
            return;
        
        smallestDimension = Math.Min(page.Width * 0.85, page.Height * 0.8);

        UxChessboardView.WidthRequest = smallestDimension;
        UxChessboardView.HeightRequest = smallestDimension;
        AdjustChessboardPieces();
    }

    private double smallestDimension;
    private int width;
    public int Width
    {
        get => width;
        set
        {
            if (width != value)
            {
                width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }

    private int height;
    public int Height
    {
        get => height;
        set
        {
            if (height != value)
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
    }
    private void AdjustChessboardPieces()
    {
        var size = UxChessboardView.Width / 8; 
        foreach (ChessboardSquare item in (IEnumerable<ChessboardSquare>)UxChessboardView.ItemsSource)
        {
            item.Width = (int)size;
            item.Height = (int)size;
        }
    }
    
    private void ChessboardView_OnSizeChanged(object sender, EventArgs e)
    {
        if (sender is CollectionView collectionView && isLoaded)
        {
            AdjustChessboardPieces();
        }
    }
}