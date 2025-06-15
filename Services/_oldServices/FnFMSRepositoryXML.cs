// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Threading.Tasks;

// /// <summary>
// /// Repository for accessing and manipulating file and folder entities in the database.
// /// </summary>
// public class FnFMSRepository : IFnFMSRepository
// {
//     private readonly AppDbContext _context;

//     /// <summary>
//     /// Initializes a new instance of the <see cref="FnFMSRepository"/> class.
//     /// </summary>
//     /// <param name="context">The application database context.</param>
//     public FnFMSRepository(AppDbContext context)
//     {
//         _context = context;
//     }

//     /// <summary>
//     /// Retrieves a user by ID, including their folders.
//     /// </summary>
//     /// <param name="userId">The unique ID of the user.</param>
//     /// <returns>The <see cref="UserEntity"/> if found; otherwise, <c>null</c>.</returns>
//     public async Task<UserEntity?> GetUserByIdAsync(Guid userId)
//     {
//         return await _context.Users
//             .Include(u => u.Folders)
//             .FirstOrDefaultAsync(u => u.Id == userId);
//     }

//     /// <summary>
//     /// Retrieves a folder by ID, including its nested folders and files.
//     /// </summary>
//     /// <param name="folderId">The unique ID of the folder.</param>
//     /// <returns>The <see cref="FolderEntity"/> if found; otherwise, <c>null</c>.</returns>
//     public async Task<FolderEntity?> GetFolderByIdAsync(Guid folderId)
//     {
//         return await _context.Folders
//             .Include(f => f.Folders)
//             .Include(f => f.Files)
//             .FirstOrDefaultAsync(f => f.FolderId == folderId);
//     }

//     /// <summary>
//     /// Retrieves a file by ID, including its parent folder.
//     /// </summary>
//     /// <param name="fileId">The unique ID of the file.</param>
//     /// <returns>The <see cref="FileEntity"/> if found; otherwise, <c>null</c>.</returns>
//     public async Task<FileEntity?> GetFileByIdAsync(Guid fileId)
//     {
//         return await _context.Files
//             .Include(f => f.ParentFolder)
//             .FirstOrDefaultAsync(f => f.FileId == fileId);
//     }

//     /// <summary>
//     /// Creates and saves a new root folder in the database.
//     /// </summary>
//     /// <param name="newRootfolder">The new root folder entity.</param>
//     /// <returns>The created <see cref="FolderEntity"/>.</returns>
//     public async Task<FolderEntity> CreateRootFolderAsync(FolderEntity newRootfolder)
//     {
//         _context.Folders.Add(newRootfolder);
//         await _context.SaveChangesAsync();
//         return newRootfolder;
//     }

//     /// <summary>
//     /// Creates and saves a new nested folder in the database.
//     /// </summary>
//     /// <param name="newNestedFolder">The new nested folder entity.</param>
//     /// <returns>The created <see cref="FolderEntity"/>.</returns>
//     public async Task<FolderEntity> CreateNestedFolderAsync(FolderEntity newNestedFolder)
//     {
//         _context.Folders.Add(newNestedFolder);
//         await _context.SaveChangesAsync();
//         return newNestedFolder;
//     }

//     /// <summary>
//     /// Updates an existing folder's name.
//     /// </summary>
//     /// <param name="updatedFolderName">The folder entity with the updated name.</param>
//     public async Task UpdateFolderAsync(FolderEntity updatedFolderName)
//     {
//         _context.Folders.Update(updatedFolderName);
//         await _context.SaveChangesAsync();
//     }

//     /// <summary>
//     /// Deletes a folder from the database.
//     /// </summary>
//     /// <param name="folderEntity">The folder entity to delete.</param>
//     public async Task DeleteFolderAsync(FolderEntity folderEntity)
//     {
//         _context.Folders.Remove(folderEntity);
//         await _context.SaveChangesAsync();
//     }

//     /// <summary>
//     /// Saves a new file to the specified folder.
//     /// </summary>
//     /// <param name="newFile">The file entity to save.</param>
//     /// <returns>The saved <see cref="FileEntity"/>.</returns>
//     public async Task<FileEntity> SaveFileAsync(FileEntity newFile)
//     {
//         _context.Files.Add(newFile);
//         await _context.SaveChangesAsync();
//         return newFile;
//     }

//     /// <summary>
//     /// Updates the metadata (e.g., name) of an existing file.
//     /// </summary>
//     /// <param name="updatedFile">The updated file entity.</param>
//     public async Task UpdateFileAsync(FileEntity updatedFile)
//     {
//         _context.Files.Update(updatedFile);
//         await _context.SaveChangesAsync();
//     }

//     /// <summary>
//     /// Deletes a file from the database.
//     /// </summary>
//     /// <param name="file">The file entity to delete.</param>
//     public async Task DeleteFileAsync(FileEntity file)
//     {
//         _context.Files.Remove(file);
//         await _context.SaveChangesAsync();
//     }
// }
