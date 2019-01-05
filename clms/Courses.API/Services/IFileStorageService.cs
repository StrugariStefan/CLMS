using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.API.Services
{
    public interface IFileStorageService
    {
        Task UploadFile(Guid id, Stream stream, string fileName);
        Task<MemoryStream> DownloadFile(Guid id, string fileName);
    }
}
