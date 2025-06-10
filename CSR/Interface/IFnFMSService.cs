using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFnFMSService
{
    // Folder Operations
    Task<Guid> CreateRootFolderAsync(CreateRootFolderRequest request);
    Task<Guid> CreateFolderInFolderAsync(CreateFolderInFolderRequest request);
    Task<bool> UpdateFolderNameAsync(UpdateFolderNameRequest request);
    Task<bool> DeleteFolderAsync(DeleteFolderRequest request);

    // File Operations
    Task<Guid> UploadFileToFolderAsync(UploadFileToFolderRequest request);
    Task<DownloadFileFromFolderResponse?> DownloadFileFromFolderAsync(Guid fileId);
    Task<bool> UpdateFileNameAsync(UpdateFileNameRequest request);
    Task<bool> DeleteFileAsync(DeleteFileRequest request);
}
