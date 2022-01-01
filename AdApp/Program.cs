using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;

//DownloadBlobWithConnectionString();
//DownloadBlobWithAdApp();
DownloadBlobWithManagedIdentity();



void DownloadBlobWithConnectionString()
{
    const string blobConnectionString = "azure storage connection string here";
    const string containerName = "data";
    const string localBlob = "c:\\mnl\\MSR2.html";
    const string blobName = "MSR2.html";

    var blobServiceClient = new BlobServiceClient(blobConnectionString);
    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    var blobClient = containerClient.GetBlobClient(blobName);
    blobClient.DownloadTo(localBlob);
    Console.WriteLine("Blob Downloaded");

}
void DownloadBlobWithAdApp()
{
    const string tenantId = "tenant id here";
    const string clientId = "client id here";
    const string clientSecret = "secret here";
    const string localBlob = "c:\\mnl\\MSR2.html";

    var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    var blobUri = new Uri("https://mnl2000.blob.core.windows.net/data/MSR2.html");
    var blobClient = new BlobClient(blobUri, clientSecretCredential);
    blobClient.DownloadTo(localBlob);
    Console.WriteLine("Blob Downloaded");

}
void DownloadBlobWithManagedIdentity()
{
    const string localBlob = "c:\\mnl\\MSR2.html";
    var credential = new ChainedTokenCredential(new AzureCliCredential(),new ManagedIdentityCredential());
    var blobUri = new Uri("https://mnl2000.blob.core.windows.net/data/MSR2.html");
    var blobClient = new BlobClient(blobUri, credential);
    blobClient.DownloadTo(localBlob);
    Console.WriteLine("Blob Downloaded");

}


