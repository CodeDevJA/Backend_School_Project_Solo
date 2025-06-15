public interface IFileService
{
    // Method - File - UploadFileToFolderAsync
    Task<UploadFileResponseDto?> UploadFileToFolderAsync(Guid userId, UploadFileRequestDto request);

    // Method - File - DownloadFileFromFolderAsync
    Task<DownloadFileResponseDto?> DownloadFileFromFolderAsync(Guid userId, Guid fileId);

    // Method - File - UpdateFileNameAsync
    Task<bool> UpdateFileNameAsync(Guid userId, Guid fileId, string newFilename);

    // Method - File - DeleteFileAsync
    Task<bool> DeleteFileAsync(Guid userId, Guid fileId);
}
