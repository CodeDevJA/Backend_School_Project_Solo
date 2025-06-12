using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public interface IFnFMSRepository
{
    // Method - User - GetUserById - NavigationObject
    Task<UserEntity?> GetUserByIdAsync(Guid userId);

    // Method - Folder - CreateRootFolderAsync
    Task<FolderEntity> CreateRootFolderAsync(FolderEntity folder);
    // Method - Folder - CreateFolderInFolderAsync
    // Method - Folder - UpdateFolderNameAsync
    // Method - Folder - DeleteFolderAsync

    // Method - File - UploadFileToFolderAsync
    // Method - File - DownloadFileFromFolderAsync
    // Method - File - UpdateFileNameAsync
    // Method - File - DeleteFileAsync
}

public class FnFMSRepository : IFnFMSRepository
{
    private readonly AppDbContext _context;

    public FnFMSRepository(AppDbContext context)
    {
        _context = context;
    }

    // Method - User - GetUserById - NavigationObject
    public async Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Folders) // Include folders if needed
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    // Method - Folder - CreateRootFolder
    public async Task<FolderEntity> CreateRootFolderAsync(FolderEntity newRootfolder)
    {
        _context.Folders.Add(newRootfolder);
        await _context.SaveChangesAsync();
        return newRootfolder;
    }

    // Method - Folder - CreateFolderInFolder
    // Method - Folder - UpdateFolderName
    // Method - Folder - DeleteFolder

    // Method - File - UploadFileToFolder
    // Method - File - DownloadFileFromFolder
    // Method - File - UpdateFileName
    // Method - File - DeleteFile
}
