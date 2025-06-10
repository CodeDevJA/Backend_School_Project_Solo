using System;
using System.Threading.Tasks;

public class FnFMSService : IFnFMSService
{
    private readonly IFnFMSRepository _repository;

    public FnFMSService(IFnFMSRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateRootFolderResponseDto> CreateRootFolderAsync(string folderName)
    {
        try
        {
            var folder = await _repository.CreateRootFolderAsync(folderName);
            return new CreateRootFolderResponseDto { FolderId = folder.FolderId, FolderName = folder.Foldername };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating root folder: {ex.Message}");
        }
    }

    public async Task<CreateFolderInFolderResponseDto> CreateFolderInFolderAsync(CreateFolderInFolderRequestDto request)
    {
        try
        {
            var folder = await _repository.CreateFolderInFolderAsync(request.FolderName, request.ParentFolderId);
            return new CreateFolderInFolderResponseDto
            {
                FolderId = folder.FolderId,
                FolderName = folder.Foldername,
                ParentFolderId = folder.ParentFolderId.Value
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating folder in folder: {ex.Message}");
        }
    }

    public async Task<UpdateFolderNameResponseDto> UpdateFolderNameAsync(Guid folderId, UpdateFolderNameRequestDto request)
    {
        try
        {
            var updatedFolder = await _repository.UpdateFolderNameAsync(folderId, request.NewFolderName);
            return new UpdateFolderNameResponseDto { FolderId = updatedFolder.FolderId, UpdatedFolderName = updatedFolder.Foldername };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating folder name: {ex.Message}");
        }
    }

    public async Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid folderId)
    {
        try
        {
            var isDeleted = await _repository.DeleteFolderAsync(folderId);
            return new DeleteFolderResponseDto { FolderId = folderId, IsDeleted = isDeleted };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting folder: {ex.Message}");
        }
    }

    public async Task<UploadFileToFolderResponseDto> UploadFileToFolderAsync(UploadFileToFolderRequestDto request)
    {
        try
        {
            var file = await _repository.UploadFileToFolderAsync(request.Filename, request.FileContent, request.ParentFolderId);
            return new UploadFileToFolderResponseDto
            {
                FileId = file.FileId,
                Filename = file.Filename,
                ParentFolderId = file.ParentFolderId
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading file to folder: {ex.Message}");
        }
    }

    public async Task<DownloadFileFromFolderResponseDto> DownloadFileFromFolderAsync(Guid fileId)
    {
        try
        {
            var file = await _repository.DownloadFileFromFolderAsync(fileId);
            return new DownloadFileFromFolderResponseDto
            {
                FileId = file.FileId,
                Filename = file.Filename,
                FileContent = file.FileContent
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error downloading file from folder: {ex.Message}");
        }
    }

    public async Task<UpdateFileNameResponseDto> UpdateFileNameAsync(Guid fileId, UpdateFileNameRequestDto request)
    {
        try
        {
            var updatedFile = await _repository.UpdateFileNameAsync(fileId, request.NewFilename);
            return new UpdateFileNameResponseDto { FileId = updatedFile.FileId, UpdatedFilename = updatedFile.Filename };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating file name: {ex.Message}");
        }
    }

    public async Task<DeleteFileResponseDto> DeleteFileAsync(Guid fileId)
    {
        try
        {
            var isDeleted = await _repository.DeleteFileAsync(fileId);
            return new DeleteFileResponseDto { FileId = fileId, IsDeleted = isDeleted };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting file: {ex.Message}");
        }
    }
}
