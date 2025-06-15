public interface IFnFMSService
{

    // Method - Folder - CreateRootFolderAsync
    Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request);

    // Method - Folder - CreateFolderInFolderAsync
    Task<FolderResponseDto?> CreateFolderInFolderAsync(Guid userId, CreateFolderInFolderRequestDto request);

    // Method - Folder - UpdateFolderNameAsync
    Task<FolderResponseDto?> UpdateFolderNameAsync(Guid userId, UpdateFolderNameRequestDto request);

    // Method - Folder - DeleteFolderAsync
    Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid userId, DeleteFolderRequestDto request);


    // Method - File - UploadFileToFolderAsync
    Task<UploadFileResponseDto?> UploadFileToFolderAsync(Guid userId, UploadFileRequestDto request);

    // Method - File - DownloadFileFromFolderAsync
    Task<DownloadFileResponseDto?> DownloadFileFromFolderAsync(Guid userId, Guid fileId);

    // Method - File - UpdateFileNameAsync
    Task<bool> UpdateFileNameAsync(Guid userId, Guid fileId, string newFilename);

    // Method - File - DeleteFileAsync
    Task<bool> DeleteFileAsync(Guid userId, Guid fileId);
}
