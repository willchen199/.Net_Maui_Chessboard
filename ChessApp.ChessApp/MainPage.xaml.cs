using System;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.Controls;

namespace ChessApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        MakeChessboard();
        MakeChessboard();
    }

    private void MakeChessboard()
    {
        ImageButton chessGridSquare = new ImageButton()
        {
            Source = "bishop_b.png",
            BackgroundColor = Colors.White
        };
        
        UxChessGrid.Children.Add(chessGridSquare);
    }

/*    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }*/
private void ImageButton_OnClicked(object sender, EventArgs e)
{
    throw new NotImplementedException();
}
}