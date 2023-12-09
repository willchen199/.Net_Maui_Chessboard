namespace ChessApp
{
    public partial class PausePage : ContentPage
    {
        public PausePage()
        {
            InitializeComponent();
        }

        // Additional code for handling events or other logic in PausePage

        private async void MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
