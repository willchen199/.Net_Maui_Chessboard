namespace ChessApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ChessboardVM();
    }

    private void ImageButton_OnClicked(object sender, EventArgs e)
    {
        if (sender.GetType() == typeof(ImageButton))
        {
            ImageButton pressedButton = (ImageButton)sender;
            pressedButton.Source = ImageSource.FromFile("black_tile.png");
        }
    }
    
    public double MaxValue {
        get => (double)GetValue(maxValueProperty);
        set => SetValue(maxValueProperty, value);
    }

    private readonly BindableProperty maxValueProperty =
        BindableProperty.Create("MaxValue", typeof(double), typeof(MainPage), 45);

    private void OnCollectionViewSizeChanged(object sender, EventArgs e)
    {
        var collectionView = sender as CollectionView;
        if (collectionView != null)
        {
            var size = collectionView.Width / 8; // Assuming collectionView.Width == collectionView.Height
            foreach (ChessboardSquare item in (collectionView.ItemsSource as IEnumerable<ChessboardSquare>)!)
            {
                item.Width = (int)size;
                item.Height = (int)size;
            }
        }
    }
}