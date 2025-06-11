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
