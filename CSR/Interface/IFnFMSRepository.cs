using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFnFMSRepository
{
    // Folder-methods
    Task<Guid> CreateRootFolderAsync(FolderEntity folder);
    Task<Guid> CreateFolderInFolderAsync(FolderEntity folder);
    Task<bool> UpdateFolderNameAsync(Guid folderId, string newFolderName);
    Task<bool> DeleteFolderAsync(Guid folderId);

    // File-methods
    Task<Guid> UploadFileToFolderAsync(FileEntity file);
    Task<FileEntity?> DownloadFileFromFolderAsync(Guid fileId);
    Task<bool> UpdateFileNameAsync(Guid fileId, string newFileName);
    Task<bool> DeleteFileAsync(Guid fileId);
}
