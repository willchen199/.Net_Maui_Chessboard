using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ChessApp;

public partial class DragDropPage : ContentPage
{
    public DragDropPage()
    {
        InitializeComponent();
    }
    
    private void DragGestureRecognizer_DragStarting_1(object sender, DragStartingEventArgs e)
    {
        var label = (sender as Element)?.Parent as Label;
        e.Data.Properties.Add("Text", label.Text);
    }

    private void DropGestureRecognizer_Drop_1(object sender, DropEventArgs e)
    {
        var data = e.Data.Properties["Text"].ToString();
        var frame = (sender as Element)?.Parent as Frame;
        frame.Content = new Label
        {
            Text = data
        };
    }

    private async void Button_OnPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}