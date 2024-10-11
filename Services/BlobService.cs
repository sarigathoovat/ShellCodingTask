using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ShellCodingTask.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
            _containerName = configuration["AzureBlobStorage:ContainerName"];
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainer.CreateIfNotExistsAsync();
            var blobClient = blobContainer.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream);
            return blobClient.Uri.ToString();
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainer.GetBlobClient(fileName);
            return await blobClient.OpenReadAsync();
        }
        public async Task<List<string>> ListBlobsAsync()
        {
            var blobNames = new List<string>();
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobNames.Add(blobItem.Name);
            }

            return blobNames;
        }
    }
}
