public interface IFnFMSRepository
{
    // Method - User - GetUserById - NavigationObject
    Task<UserEntity?> GetUserByIdAsync(Guid userId);


    // Method - Folder - GetFolderById - NavigationObject
    Task<FolderEntity?> GetFolderByIdAsync(Guid folderId);

    // Method - Folder - CreateRootFolderAsync
    Task<FolderEntity> CreateRootFolderAsync(FolderEntity folder);

    // Method - Folder - CreateFolderInFolderAsync
    Task<FolderEntity> CreateNestedFolderAsync(FolderEntity newNestedFolder);

    // Method - Folder - UpdateFolderNameAsync
    Task UpdateFolderAsync(FolderEntity updatedFolderName);

    // Method - Folder - DeleteFolderAsync
    Task DeleteFolderAsync(FolderEntity folderEntity);


    // Method - File - GetFileById - NavigationObject
    Task<FileEntity?> GetFileByIdAsync(Guid fileId);

    // Method - File - UploadFileToFolderAsync
    Task<FileEntity> SaveFileAsync(FileEntity newFile);

    // Method - File - DownloadFileFromFolderAsync
    // Task<FileEntity?> GetFileByIdAsync(Guid fileId);

    // Method - File - UpdateFileNameAsync
    Task UpdateFileAsync(FileEntity updatedFile);

    // Method - File - DeleteFileAsync
    Task DeleteFileAsync(FileEntity file);
}
