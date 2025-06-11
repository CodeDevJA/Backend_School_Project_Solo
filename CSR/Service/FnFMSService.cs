using System;
using System.Threading.Tasks;

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
    // CreateFolderInFolder
    // UpdateFolderName
    // DeleteFolder

    // File
    // UploadFileToFolder
    // DownloadFileFromFolder
    // UpdateFileName
    // DeleteFile
}
