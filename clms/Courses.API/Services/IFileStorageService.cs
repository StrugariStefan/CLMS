using System;
using System.IO;
using System.Threading.Tasks;

namespace Courses.API.Services
{
    public interface IFileStorageService
    {
        Task UploadFile(Guid id, Stream stream, string fileName);
        Task<MemoryStream> DownloadFile(Guid id, string fileName);
        Task DeleteContainer(Guid id);
    }
}
