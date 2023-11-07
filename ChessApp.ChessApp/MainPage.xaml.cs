namespace ChessApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ChessboardVM();
        isLoaded = true;
    }

    private void ImageButton_OnClicked(object sender, EventArgs e)
    {
        if (sender.GetType() == typeof(ImageButton))
        {
            ImageButton pressedButton = (ImageButton)sender;
            pressedButton.Source = ImageSource.FromFile("black_tile.png");
        }
    }
    
    private bool isLoaded = false;
    
    private void OnCollectionViewSizeChanged(object sender, EventArgs e)
    {
        var collectionView = sender as CollectionView;
        if (collectionView != null && isLoaded)
        {
            var size = collectionView.Width / 8; // Assuming collectionView.Width is not null
            foreach (ChessboardSquare item in (collectionView.ItemsSource as IEnumerable<ChessboardSquare>))
            {
                item.Width = (int)size;
                item.Height = (int)size;
            }
        }
    }
}