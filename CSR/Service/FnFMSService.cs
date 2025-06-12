using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public interface IFnFMSService
{

    // Method - Folder - CreateRootFolderAsync
    Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request);
    // Method - Folder - CreateFolderInFolderAsync
    // Method - Folder - UpdateFolderNameAsync
    // Method - Folder - DeleteFolderAsync

    // Method - File - UploadFileToFolderAsync
    // Method - File - DownloadFileFromFolderAsync
    // Method - File - UpdateFileNameAsync
    // Method - File - DeleteFileAsync
}

public class FnFMSService : IFnFMSService
{
    private readonly IFnFMSRepository _repository;

    public FnFMSService(IFnFMSRepository repository)
    {
        _repository = repository;
    }

    // Method - Folder - CreateRootFolderAsync
    public async Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request)
    {
        // Fetch the userEntity (navigation object) from the database
        var userEntity = await _repository.GetUserByIdAsync(userId);
        if (userEntity is null)
        {
            throw new Exception("User not found.");
        }

        // Assign values and construct FolderEntity
        var newRootFolder = new FolderEntity
        {
            FolderId = Guid.NewGuid(),
            Foldername = request.FolderName,
            Folders = new List<FolderEntity>(),
            Files = new List<FileEntity>(),
            ParentFolderId = null,
            ParentFolder = null,  // Root folders don't have a parent
            ParentUserId = userId,
            ParentUser = userEntity
        };

        // Save FolderEntity to database
        var createdRootFolder = await _repository.CreateRootFolderAsync(newRootFolder);

        // Return response DTO
        return new FolderResponseDto
        {
            FolderId = createdRootFolder.FolderId,
            FolderName = createdRootFolder.Foldername
        };
    }

    // Method - Folder - CreateFolderInFolderAsync
    // Method - Folder - UpdateFolderNameAsync
    // Method - Folder - DeleteFolderAsync

    // Method - File - UploadFileToFolderAsync
    // Method - File - DownloadFileFromFolderAsync
    // Method - File - UpdateFileNameAsync
    // Method - File - DeleteFileAsync
}
