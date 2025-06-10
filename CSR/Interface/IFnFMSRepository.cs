using System;
using System.Threading.Tasks;

public interface IFnFMSRepository
{
    Task<FolderEntity> CreateRootFolderAsync(string folderName);
    Task<FolderEntity> CreateFolderInFolderAsync(string folderName, Guid parentFolderId);
    Task<FolderEntity> UpdateFolderNameAsync(Guid folderId, string newFolderName);
    Task<bool> DeleteFolderAsync(Guid folderId);

    Task<FileEntity> UploadFileToFolderAsync(string filename, byte[] fileContent, Guid parentFolderId);
    Task<FileEntity> DownloadFileFromFolderAsync(Guid fileId);
    Task<FileEntity> UpdateFileNameAsync(Guid fileId, string newFilename);
    Task<bool> DeleteFileAsync(Guid fileId);
}
