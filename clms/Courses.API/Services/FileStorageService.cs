using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Courses.API.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly CloudStorageAccount _storageAccount = null;
        private readonly CloudBlobClient _blobClient = null;

        private const string StorageConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=clms;AccountKey=vmUpBVnJ3T90Pt3pp+W8Qu2TfMoDEaLqdctaUPDMpN7fZ35Knz6S1ctGhfq6JFSE5t1rD5pjOyp/mUJLP+ZonQ==;EndpointSuffix=core.windows.net";

        public FileStorageService()
        {
            _storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        public async Task UploadFile(Guid id, Stream stream, string fileName)
        {
            CloudBlobContainer blobContainer = _blobClient.GetContainerReference("couses");
            await blobContainer.CreateAsync();
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(id.ToString());
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public Task DownloadFile(Guid id, out Stream destinationStream, out string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
