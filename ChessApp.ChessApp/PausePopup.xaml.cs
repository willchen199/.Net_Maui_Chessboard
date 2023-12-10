using ChessApp.DataBaseAccess;
using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;

namespace ChessApp
{
    public partial class PausePopup : Popup
    {
        private ChessboardVM _chessboardVM;

        public PausePopup(ChessboardVM chessboardVM)
        {
            InitializeComponent();
            _chessboardVM = chessboardVM;
        }

        private string SerializeChessboardVM()
        {
            return JsonConvert.SerializeObject(_chessboardVM);
        }
        
        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }
        
        private void GoToMainPage(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "NavigateToMainPage");
            Close();
        }

        private async void SaveSlot1_OnClicked(object sender, EventArgs e)
        {
            string json = SerializeChessboardVM();
            await AccessAzureBlob.UploadFromStringAsync("testUser", "testBlobOne", json);
        }
    }
}
