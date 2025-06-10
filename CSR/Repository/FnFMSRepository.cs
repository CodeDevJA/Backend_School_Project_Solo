using Microsoft.EntityFrameworkCore;

public class FnFMSRepository : IFnFMSRepository
{
    private readonly AppDbContext _context;

    public FnFMSRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Guid> CreateRootFolderAsync(FolderEntity folder)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateFolderInFolderAsync(FolderEntity folder)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateFolderNameAsync(Guid folderId, string newFolderName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFolderAsync(Guid folderId)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UploadFileToFolderAsync(FileEntity file)
    {
        throw new NotImplementedException();
    }

    public Task<FileEntity?> DownloadFileFromFolderAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateFileNameAsync(Guid fileId, string newFileName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFileAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }
}
