using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public interface IFnFMSRepository
{
    // Method - User - GetUserById - NavigationObject
    Task<UserEntity?> GetUserByIdAsync(Guid userId);

    // Method - Folder - GetFolderById - NavigationObject
    Task<FolderEntity?> GetFolderByIdAsync(Guid folderId);

    // Method - Folder - CreateRootFolderAsync
    Task<FolderEntity> CreateRootFolderAsync(FolderEntity folder);

    // Method - Folder - CreateFolderInFolderAsync
    Task<FolderEntity> CreateNestedFolderAsync(FolderEntity newNestedFolder);

    // Method - Folder - UpdateFolderNameAsync
    Task UpdateFolderAsync(FolderEntity updatedFolderName);

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

    // Method - Folder - GetFolderById - NavigationObject
    public async Task<FolderEntity?> GetFolderByIdAsync(Guid folderId)
    {
        return await _context.Folders
            .Include(f => f.Folders) // Include nested folders if needed
            .Include(f => f.Files) // Include files if needed
            .FirstOrDefaultAsync(f => f.FolderId == folderId);
    }

    // Method - Folder - CreateRootFolder
    public async Task<FolderEntity> CreateRootFolderAsync(FolderEntity newRootfolder)
    {
        _context.Folders.Add(newRootfolder);
        await _context.SaveChangesAsync();
        return newRootfolder;
    }

    // Method - Folder - CreateFolderInFolder
    public async Task<FolderEntity> CreateNestedFolderAsync(FolderEntity newNestedFolder)
    {
        _context.Folders.Add(newNestedFolder);
        await _context.SaveChangesAsync();
        return newNestedFolder;
    }

    // Method - Folder - UpdateFolderName
    public async Task UpdateFolderAsync(FolderEntity updatedFolderName)
    {
        _context.Folders.Update(updatedFolderName);
        await _context.SaveChangesAsync();
    }

    // Method - Folder - DeleteFolder

    // Method - File - UploadFileToFolder
    // Method - File - DownloadFileFromFolder
    // Method - File - UpdateFileName
    // Method - File - DeleteFile
}
