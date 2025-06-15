using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class FolderService : IFolderService
{
    private readonly IFnFMSRepository _repository;

    public FolderService(IFnFMSRepository repository)
    {
        _repository = repository;
    }

    // Method - Folder - CreateRootFolderAsync
    /// <summary>
    /// Creates a new root folder for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user creating the folder.</param>
    /// <param name="request">The folder creation request DTO.</param>
    /// <returns>A response DTO containing the created folder's information.</returns>
    public async Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request)
    {
        // Fetch the user entity (navigation object) from the database
        var userEntity = await _repository.GetUserByIdAsync(userId);
        if (userEntity is null)
        {
            throw new Exception("User not found.");
        }

        // Assign values and construct folder entity
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

        // Save folder entity to database
        var createdRootFolder = await _repository.CreateRootFolderAsync(newRootFolder);

        // Return response DTO
        return new FolderResponseDto
        {
            FolderId = createdRootFolder.FolderId,
            FolderName = createdRootFolder.Foldername
        };
    }

    // Method - Folder - CreateFolderInFolderAsync
    /// <summary>
    /// Creates a new folder inside an existing parent folder.
    /// </summary>
    /// <param name="userId">The ID of the user creating the folder.</param>
    /// <param name="request">The request DTO containing parent folder ID and new folder name.</param>
    /// <returns>A response DTO containing the created nested folder's details.</returns>
    public async Task<FolderResponseDto?> CreateFolderInFolderAsync(Guid userId, CreateFolderInFolderRequestDto request)
    {
        // Fetch the user entity (navigation object) from the database
        var userEntity = await _repository.GetUserByIdAsync(userId);
        if (userEntity is null) throw new Exception("User not found.");

        // Fetch parent folder entity (navigation object) from the database
        var parentFolder = await _repository.GetFolderByIdAsync(request.ParentFolderId);
        if (parentFolder is null) throw new Exception("Parent folder not found.");

        // Assign values and construct folder entity
        var newNestedFolder = new FolderEntity
        {
            FolderId = Guid.NewGuid(),
            Foldername = request.FolderName,
            Folders = new List<FolderEntity>(),
            Files = new List<FileEntity>(),
            ParentFolderId = request.ParentFolderId,
            ParentFolder = parentFolder,
            ParentUserId = userId,
            ParentUser = userEntity
        };

        // Save folder entity to database
        var createdNestedFolder = await _repository.CreateNestedFolderAsync(newNestedFolder);

        // Return response DTO
        return new FolderResponseDto
        {
            FolderId = createdNestedFolder.FolderId,
            FolderName = createdNestedFolder.Foldername
        };
    }

    // Method - Folder - UpdateFolderNameAsync
    /// <summary>
    /// Updates the name of an existing folder.
    /// </summary>
    /// <param name="userId">The ID of the user requesting the update.</param>
    /// <param name="request">The request DTO containing the folder ID and new name.</param>
    /// <returns>A response DTO with the updated folder's information.</returns>
    public async Task<FolderResponseDto?> UpdateFolderNameAsync(Guid userId, UpdateFolderNameRequestDto request)
    {
        // Fetch folder entity
        var folderEntity = await _repository.GetFolderByIdAsync(request.FolderId);
        if (folderEntity is null || folderEntity.ParentUserId != userId)
        {
            throw new Exception("Folder not found or unauthorized.");
        }

        // Update folder name
        folderEntity.Foldername = request.NewFolderName;
        await _repository.UpdateFolderAsync(folderEntity);

        // Return response DTO
        return new FolderResponseDto
        {
            FolderId = folderEntity.FolderId,
            FolderName = folderEntity.Foldername
        };
    }

    // Method - Folder - DeleteFolderAsync
    /// <summary>
    /// Deletes the specified folder for the given user.
    /// </summary>
    /// <param name="userId">The ID of the user deleting the folder.</param>
    /// <param name="request">The request DTO containing the folder ID.</param>
    /// <returns>A response DTO indicating whether the folder was deleted.</returns>
    public async Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid userId, DeleteFolderRequestDto request)
    {
        // Fetch folder entity
        var folderEntity = await _repository.GetFolderByIdAsync(request.FolderId);
        if (folderEntity is null || folderEntity.ParentUserId != userId)
        {
            return new DeleteFolderResponseDto
            {
                Success = false,
                Message = "Folder not found or unauthorized."
            };
        }

        // Delete folder
        await _repository.DeleteFolderAsync(folderEntity);

        return new DeleteFolderResponseDto
        {
            Success = true,
            Message = "Folder deleted successfully."
        };
    }
}
