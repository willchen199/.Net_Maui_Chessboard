using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ChessApp.DataBaseAccess;

public class AccessAzureBlob
{
    public static async Task UploadFromStringAsync(string userName, string blobName, string stringToUpload)
    {
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        BlobContainerClient containerClient = new BlobContainerClient(connectionString, ContainerName);
        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient($"{userName}/{blobName}");
        await blobClient.UploadAsync(BinaryData.FromString(stringToUpload), overwrite: true);
    }

    public static async Task<string> DownloadToStringAsync(string userName, string blobName)
    {
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        BlobContainerClient containerClient = new BlobContainerClient(connectionString, ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient($"{userName}/{blobName}");
        BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
        return downloadResult.Content.ToString();
    }

    private const string ContainerName = "applied-programming";
}