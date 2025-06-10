

public class FnFMSService : IFnFMSService
{
    private readonly FnFMSRepository _repository;

    public FnFMSService(FnFMSRepository repository)
    {
        _repository = repository;
    }

    public Task<Guid> CreateRootFolderAsync(CreateRootFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateFolderInFolderAsync(CreateFolderInFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateFolderNameAsync(UpdateFolderNameRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFolderAsync(DeleteFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UploadFileToFolderAsync(UploadFileToFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<DownloadFileFromFolderResponse?> DownloadFileFromFolderAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateFileNameAsync(UpdateFileNameRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFileAsync(DeleteFileRequest request)
    {
        throw new NotImplementedException();
    }
}
