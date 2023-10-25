using System.Collections.ObjectModel;

namespace ChessApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ChessboardSquareVM> ChessboardVM { get; set; }

    public MainPage()
    {
        InitializeComponent();
        ChessboardVM = new ChessboardVM().Squares;
    }
}