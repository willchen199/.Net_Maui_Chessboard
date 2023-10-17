namespace ChessApp;

public partial class MainPage : ContentPage
{
    private Chessboard chessboard;

    public MainPage()
    {
        InitializeComponent();
        chessboard = new Chessboard(ChessboardGrid);
    }
}