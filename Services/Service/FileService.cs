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
