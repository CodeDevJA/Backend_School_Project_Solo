public interface IFolderService
{

    // Method - Folder - CreateRootFolderAsync
    Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request);

    // Method - Folder - CreateFolderInFolderAsync
    Task<FolderResponseDto?> CreateFolderInFolderAsync(Guid userId, CreateFolderInFolderRequestDto request);

    // Method - Folder - UpdateFolderNameAsync
    Task<FolderResponseDto?> UpdateFolderNameAsync(Guid userId, UpdateFolderNameRequestDto request);

    // Method - Folder - DeleteFolderAsync
    Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid userId, DeleteFolderRequestDto request);
}
