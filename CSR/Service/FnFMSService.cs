using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

    // Method - File - UploadFileToFolderAsync
    public async Task<UploadFileResponseDto?> UploadFileToFolderAsync(Guid userId, UploadFileRequestDto request)
    {
        // Fetch parent folder entity
        var parentFolder = await _repository.GetFolderByIdAsync(request.ParentFolderId);
        if (parentFolder is null || parentFolder.ParentUserId != userId)
        {
            throw new Exception("Folder not found or unauthorized.");
        }

        // Convert IFormFile to Byte[] Array
        using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        // Create FileEntity
        var newFile = new FileEntity
        {
            FileId = Guid.NewGuid(),
            Filename = request.File.FileName,
            FileContent = fileBytes,
            ParentFolderId = request.ParentFolderId,
            ParentFolder = parentFolder
        };

        // Save file to database
        var savedFile = await _repository.SaveFileAsync(newFile);

        return new UploadFileResponseDto
        {
            FileId = savedFile.FileId,
            Filename = savedFile.Filename
        };
    }

    // Method - File - DownloadFileFromFolderAsync
    public async Task<DownloadFileResponseDto?> DownloadFileFromFolderAsync(Guid userId, Guid fileId)
    {
        var file = await _repository.GetFileByIdAsync(fileId);

        if (file is null || file.ParentFolder?.ParentUserId != userId)
        {
            return null; // Unauthorized or nonexistent file
        }

        return new DownloadFileResponseDto
        {
            FileId = file.FileId,
            Filename = file.Filename,
            Content = file.FileContent
        };
    }

    // Method - File - UpdateFileNameAsync
    public async Task<bool> UpdateFileNameAsync(Guid userId, Guid fileId, string newFilename)
    {
        var file = await _repository.GetFileByIdAsync(fileId);

        if (file is null || file.ParentFolder?.ParentUserId != userId)
        {
            return false;
        }

        file.Filename = newFilename;
        await _repository.UpdateFileAsync(file);

        return true;
    }

    // Method - File - DeleteFileAsync
    public async Task<bool> DeleteFileAsync(Guid userId, Guid fileId)
    {
        var file = await _repository.GetFileByIdAsync(fileId);

        if (file is null || file.ParentFolder?.ParentUserId != userId)
        {
            return false;
        }

        await _repository.DeleteFileAsync(file);
        return true;
    }
}
