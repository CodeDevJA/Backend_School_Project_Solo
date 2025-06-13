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
    Task DeleteFolderAsync(FolderEntity folderEntity);


    // Method - File - GetFileById - NavigationObject
    Task<FileEntity?> GetFileByIdAsync(Guid fileId);

    // Method - File - UploadFileToFolderAsync
    Task<FileEntity> SaveFileAsync(FileEntity newFile);

    // Method - File - DownloadFileFromFolderAsync
    // Task<FileEntity?> GetFileByIdAsync(Guid fileId);

    // Method - File - UpdateFileNameAsync
    Task UpdateFileAsync(FileEntity updatedFile);

    // Method - File - DeleteFileAsync
    Task DeleteFileAsync(FileEntity file);
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

    // Method - File - GetFileById - NavigationObject
    public async Task<FileEntity?> GetFileByIdAsync(Guid fileId)
    {
        return await _context.Files
            .Include(f => f.ParentFolder)
            .FirstOrDefaultAsync(f => f.FileId == fileId);
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
    public async Task DeleteFolderAsync(FolderEntity folderEntity)
    {
        _context.Folders.Remove(folderEntity);
        await _context.SaveChangesAsync();
    }


    // Method - File - UploadFileToFolder
    public async Task<FileEntity> SaveFileAsync(FileEntity newFile)
    {
        _context.Files.Add(newFile);
        await _context.SaveChangesAsync();
        return newFile;
    }

    // Method - File - DownloadFileFromFolder
    // public async Task<FileEntity?> GetFileByIdAsync(Guid fileId)

    // Method - File - UpdateFileName
    public async Task UpdateFileAsync(FileEntity updatedFile)
    {
        _context.Files.Update(updatedFile);
        await _context.SaveChangesAsync();
    }

    // Method - File - DeleteFile
    public async Task DeleteFileAsync(FileEntity file)
    {
        _context.Files.Remove(file);
        await _context.SaveChangesAsync();
    }
}
