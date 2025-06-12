using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class FnFMSService : IFnFMSService
{
    private readonly IFnFMSRepository _repository;

    public FnFMSService(IFnFMSRepository repository)
    {
        _repository = repository;
    }

    // Methods
    // Folder
    // CreateRootFolder
    public async Task<CreateRootFolderResponseDto> CreateRootFolderAsync(string userId, CreateRootFolderRequestDto request)
    {
        
    }

    // CreateFolderInFolder
    // public async Task<CreateFolderInFolderResponseDto> CreateFolderInFolderAsync(CreateFolderInFolderRequestDto request) { }

    // UpdateFolderName
    // public async Task<UpdateFolderNameResponseDto> UpdateFolderNameAsync(UpdateFolderNameRequestDto request) { }

    // DeleteFolder
    // public async Task<DeleteFolderResponseDto> DeleteFolderAsync() { }

    // File
    // UploadFileToFolder
    // public async Task<UploadFileToFolderResponseDto> UploadFileToFolderAsync(UploadFileToFolderRequestDto request) { }

    // DownloadFileFromFolder
    // public async Task<DownloadFileFromFolderResponseDto> DownloadFileFromFolderAsync() { }

    // UpdateFileName
    // public async Task<UpdateFileNameResponseDto> UpdateFileNameAsync(UpdateFileNameRequestDto request) { }

    // DeleteFile
    // public async Task<DeleteFileResponseDto> DeleteFileAsync() { }
}
