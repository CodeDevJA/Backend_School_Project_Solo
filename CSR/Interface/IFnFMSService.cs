using System;
using System.Threading.Tasks;

public interface IFnFMSService
{
    Task<CreateRootFolderResponseDto> CreateRootFolderAsync(CreateRootFolderRequestDto request);
    Task<CreateFolderInFolderResponseDto> CreateFolderInFolderAsync(CreateFolderInFolderRequestDto request);
    Task<UpdateFolderNameResponseDto> UpdateFolderNameAsync(Guid folderId, UpdateFolderNameRequestDto request);
    Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid folderId);

    Task<UploadFileToFolderResponseDto> UploadFileToFolderAsync(UploadFileToFolderRequestDto request);
    Task<DownloadFileFromFolderResponseDto> DownloadFileFromFolderAsync(Guid fileId);
    Task<UpdateFileNameResponseDto> UpdateFileNameAsync(Guid fileId, UpdateFileNameRequestDto request);
    Task<DeleteFileResponseDto> DeleteFileAsync(Guid fileId);
}
