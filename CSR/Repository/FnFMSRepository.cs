using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class FnFMSRepository : IFnFMSRepository
{
    private readonly AppDbContext _context;

    public FnFMSRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FolderEntity> CreateRootFolderAsync(string folderName)
    {
        var folder = new FolderEntity { FolderId = Guid.NewGuid(), Foldername = folderName };
        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();
        return folder;
    }

    public async Task<FolderEntity> CreateFolderInFolderAsync(string folderName, Guid parentFolderId)
    {
        var parentFolder = await _context.Folders.FindAsync(parentFolderId)
            ?? throw new Exception("Parent folder not found");

        var folder = new FolderEntity { FolderId = Guid.NewGuid(), Foldername = folderName, ParentFolderId = parentFolderId };
        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();
        return folder;
    }

    public async Task<FolderEntity> UpdateFolderNameAsync(Guid folderId, string newFolderName)
    {
        var folder = await _context.Folders.FindAsync(folderId)
            ?? throw new Exception("Folder not found");

        folder.Foldername = newFolderName;
        _context.Folders.Update(folder);
        await _context.SaveChangesAsync();
        return folder;
    }

    public async Task<bool> DeleteFolderAsync(Guid folderId)
    {
        var folder = await _context.Folders.FindAsync(folderId)
            ?? throw new Exception("Folder not found");

        _context.Folders.Remove(folder);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<FileEntity> UploadFileToFolderAsync(string filename, byte[] fileContent, Guid parentFolderId)
    {
        var parentFolder = await _context.Folders.FindAsync(parentFolderId)
            ?? throw new Exception("Parent folder not found");

        var file = new FileEntity { FileId = Guid.NewGuid(), Filename = filename, FileContent = fileContent, ParentFolderId = parentFolderId };
        await _context.Files.AddAsync(file);
        await _context.SaveChangesAsync();
        return file;
    }

    public async Task<FileEntity> DownloadFileFromFolderAsync(Guid fileId)
    {
        return await _context.Files.FindAsync(fileId)
            ?? throw new Exception("File not found");
    }

    public async Task<FileEntity> UpdateFileNameAsync(Guid fileId, string newFilename)
    {
        var file = await _context.Files.FindAsync(fileId)
            ?? throw new Exception("File not found");

        file.Filename = newFilename;
        _context.Files.Update(file);
        await _context.SaveChangesAsync();
        return file;
    }

    public async Task<bool> DeleteFileAsync(Guid fileId)
    {
        var file = await _context.Files.FindAsync(fileId)
            ?? throw new Exception("File not found");

        _context.Files.Remove(file);
        await _context.SaveChangesAsync();
        return true;
    }
}
