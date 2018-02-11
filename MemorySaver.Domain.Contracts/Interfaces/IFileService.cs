using System;
using System.IO;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;

namespace MemorySaver.Domain.ServiceContracts.Interfaces
{
    public interface IFileService
    {
        bool UploadFile(byte[] file, 
            string fileName,
            string description,
            string facebookId,
            string vimeoId,
            Guid chestId);

        bool UploadFileFromFacebook(byte[] file, string facebookId, string description, Guid chestId);

        GetFileResponseDTO GetFile(Guid fileId);

        bool DeleteFile(Guid fileId);

        byte[] GetFileForDownload(Guid id);
    }
}
