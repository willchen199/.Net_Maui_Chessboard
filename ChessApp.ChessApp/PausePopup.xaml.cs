// Importing necessary namespaces
using ChessApp.DataBaseAccess; // Namespace for database access
using CommunityToolkit.Maui.Views; // Namespace for MAUI Community Toolkit views
using Newtonsoft.Json; // Newtonsoft.Json for JSON serialization

// Namespace declaration for the ChessApp
namespace ChessApp
{
    // Partial class representing a custom PausePopup that extends Popup
    public partial class PausePopup : Popup
    {
        // Private field to store the ChessboardVM instance
        private ChessboardVM _chessboardVM;

        // Constructor for PausePopup, taking a ChessboardVM instance as a parameter
        public PausePopup(ChessboardVM chessboardVM)
        {
            // Initialize components and set the ChessboardVM instance
            InitializeComponent();
            _chessboardVM = chessboardVM;
        }

        // Method to serialize the ChessboardVM instance to a JSON string
        private string SerializeChessboardVM()
        {
            return JsonConvert.SerializeObject(_chessboardVM);
        }

        // Event handler to close the popup
        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }

        // Event handler to navigate to the main page and close the popup
        private void GoToMainPage(object sender, EventArgs e)
        {
            // Send a message to navigate to the main page
            MessagingCenter.Send(this, "NavigateToMainPage");

            // Close the popup
            Close();
        }

        // Event handler to save the current ChessboardVM to Azure Blob Storage
        private async void SaveSlot1_OnClicked(object sender, EventArgs e)
        {
            // Serialize ChessboardVM to JSON
            string json = SerializeChessboardVM();

            // Upload JSON to Azure Blob Storage
            await AccessAzureBlob.UploadFromStringAsync("testUser", "testBlobOne", json);
        }
    }
}
