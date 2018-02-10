using System;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using System.IO;
using System.Linq;
using EnsureThat;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;

namespace MemorySaver.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            Ensure.That(fileRepository).IsNotNull();

            this.fileRepository = fileRepository;
        }

        public bool UploadFile(byte[] file, string fileName, string description, string facebookId, string vimeoId, Guid chestId)
        {
            Domain.Entities.File newFile = new Domain.Entities.File
            {
                Description = description,
                ChestId = chestId,
                Type = fileName.Split('.').Last(),
                FacebookId = facebookId,
                FileName = fileName
            };

            fileRepository.Add(newFile);

            if (fileRepository.SaveChages())
            {
                string path = "C:\\LicentaFilesFolders\\" + chestId;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileExtension = fileName.Split('.').Last();
                FileStream fileStream = File.Create(path + "\\" + newFile.Id + "." + fileExtension);

                fileStream.Write(file, 0, file.Length);
                fileStream.Close();

                return true;
            }
            return false;
        }

        public GetFileResponseDTO GetFile(Guid fileId)
        {
            Domain.Entities.File fileEntity = fileRepository.GetById(fileId);
            string pathToFile = $"C:\\LicentaFilesFolders\\{fileEntity.ChestId}\\{fileEntity.Id}.{fileEntity.Type}";

            FileStream fileStream = new FileStream(pathToFile, FileMode.Open);

            byte[] fileBase64 = new byte[fileStream.Length];

            fileStream.Read(fileBase64, 0, (int)fileStream.Length);
            fileStream.Close();

            GetFileResponseDTO fileDto = new GetFileResponseDTO
            {
                FileBase64 = fileBase64,
                FileType = fileEntity.Type,
                FacebookId = fileEntity.FacebookId,
                VimeoId = fileEntity.VimeoId,
                Description = fileEntity.Description
            };

            return fileDto;

        }
        public bool DeleteFile(Guid fileId)
        {
            fileRepository.Delete(fileId);

            return fileRepository.SaveChages();
        }
    }
}
