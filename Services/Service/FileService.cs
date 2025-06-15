using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class FileService : IFileService
{
    private readonly IFnFMSRepository _repository;

    public FileService(IFnFMSRepository repository)
    {
        _repository = repository;
    }

    // Method - File - UploadFileToFolderAsync
    /// <summary>
    /// Uploads a file to the specified folder.
    /// </summary>
    /// <param name="userId">The ID of the user uploading the file.</param>
    /// <param name="request">The request DTO containing the file and folder ID.</param>
    /// <returns>A response DTO with details about the uploaded file.</returns>
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
    /// <summary>
    /// Downloads a file by ID if the user is authorized to access it.
    /// </summary>
    /// <param name="userId">The ID of the user requesting the file.</param>
    /// <param name="fileId">The ID of the file to download.</param>
    /// <returns>A response DTO containing the file content and metadata, or null if unauthorized or not found.</returns>
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
    /// <summary>
    /// Updates the name of a file if the user is authorized.
    /// </summary>
    /// <param name="userId">The ID of the user requesting the update.</param>
    /// <param name="fileId">The ID of the file to rename.</param>
    /// <param name="newFilename">The new filename.</param>
    /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
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
    /// <summary>
    /// Deletes a file if the user is authorized.
    /// </summary>
    /// <param name="userId">The ID of the user deleting the file.</param>
    /// <param name="fileId">The ID of the file to delete.</param>
    /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
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
