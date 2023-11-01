using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace ChessApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ChessboardSquareVM> ChessboardVM { get; set; }

    public MainPage()
    {
        InitializeComponent();
        ChessboardVM = new ChessboardVM().Squares;
        Console.WriteLine("break");
    }
}