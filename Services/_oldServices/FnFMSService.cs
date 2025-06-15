// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// /// <summary>
// /// Provides file and folder management operations for authenticated users.
// /// </summary>
// public class FnFMSService : IFnFMSService
// {
//     private readonly IFnFMSRepository _repository;

//     /// <summary>
//     /// Initializes a new instance of the <see cref="FnFMSService"/> class.
//     /// </summary>
//     /// <param name="repository">The repository instance used for data access.</param>
//     public FnFMSService(IFnFMSRepository repository)
//     {
//         _repository = repository;
//     }

//     /// <summary>
//     /// Creates a new root folder for the specified user.
//     /// </summary>
//     /// <param name="userId">The ID of the user creating the folder.</param>
//     /// <param name="request">The folder creation request DTO.</param>
//     /// <returns>A response DTO containing the created folder's information.</returns>
//     public async Task<FolderResponseDto> CreateRootFolderAsync(Guid userId, CreateRootFolderRequestDto request)
//     {
//         var userEntity = await _repository.GetUserByIdAsync(userId);
//         if (userEntity is null)
//         {
//             throw new Exception("User not found.");
//         }

//         var newRootFolder = new FolderEntity
//         {
//             FolderId = Guid.NewGuid(),
//             Foldername = request.FolderName,
//             Folders = new List<FolderEntity>(),
//             Files = new List<FileEntity>(),
//             ParentFolderId = null,
//             ParentFolder = null,
//             ParentUserId = userId,
//             ParentUser = userEntity
//         };

//         var createdRootFolder = await _repository.CreateRootFolderAsync(newRootFolder);

//         return new FolderResponseDto
//         {
//             FolderId = createdRootFolder.FolderId,
//             FolderName = createdRootFolder.Foldername
//         };
//     }

//     /// <summary>
//     /// Creates a new folder inside an existing parent folder.
//     /// </summary>
//     /// <param name="userId">The ID of the user creating the folder.</param>
//     /// <param name="request">The request DTO containing parent folder ID and new folder name.</param>
//     /// <returns>A response DTO containing the created nested folder's details.</returns>
//     public async Task<FolderResponseDto?> CreateFolderInFolderAsync(Guid userId, CreateFolderInFolderRequestDto request)
//     {
//         var userEntity = await _repository.GetUserByIdAsync(userId);
//         if (userEntity is null) throw new Exception("User not found.");

//         var parentFolder = await _repository.GetFolderByIdAsync(request.ParentFolderId);
//         if (parentFolder is null) throw new Exception("Parent folder not found.");

//         var newNestedFolder = new FolderEntity
//         {
//             FolderId = Guid.NewGuid(),
//             Foldername = request.FolderName,
//             Folders = new List<FolderEntity>(),
//             Files = new List<FileEntity>(),
//             ParentFolderId = request.ParentFolderId,
//             ParentFolder = parentFolder,
//             ParentUserId = userId,
//             ParentUser = userEntity
//         };

//         var createdNestedFolder = await _repository.CreateNestedFolderAsync(newNestedFolder);

//         return new FolderResponseDto
//         {
//             FolderId = createdNestedFolder.FolderId,
//             FolderName = createdNestedFolder.Foldername
//         };
//     }

//     /// <summary>
//     /// Updates the name of an existing folder.
//     /// </summary>
//     /// <param name="userId">The ID of the user requesting the update.</param>
//     /// <param name="request">The request DTO containing the folder ID and new name.</param>
//     /// <returns>A response DTO with the updated folder's information.</returns>
//     public async Task<FolderResponseDto?> UpdateFolderNameAsync(Guid userId, UpdateFolderNameRequestDto request)
//     {
//         var folderEntity = await _repository.GetFolderByIdAsync(request.FolderId);
//         if (folderEntity is null || folderEntity.ParentUserId != userId)
//         {
//             throw new Exception("Folder not found or unauthorized.");
//         }

//         folderEntity.Foldername = request.NewFolderName;
//         await _repository.UpdateFolderAsync(folderEntity);

//         return new FolderResponseDto
//         {
//             FolderId = folderEntity.FolderId,
//             FolderName = folderEntity.Foldername
//         };
//     }

//     /// <summary>
//     /// Deletes the specified folder for the given user.
//     /// </summary>
//     /// <param name="userId">The ID of the user deleting the folder.</param>
//     /// <param name="request">The request DTO containing the folder ID.</param>
//     /// <returns>A response DTO indicating whether the folder was deleted.</returns>
//     public async Task<DeleteFolderResponseDto> DeleteFolderAsync(Guid userId, DeleteFolderRequestDto request)
//     {
//         var folderEntity = await _repository.GetFolderByIdAsync(request.FolderId);
//         if (folderEntity is null || folderEntity.ParentUserId != userId)
//         {
//             return new DeleteFolderResponseDto
//             {
//                 Success = false,
//                 Message = "Folder not found or unauthorized."
//             };
//         }

//         await _repository.DeleteFolderAsync(folderEntity);

//         return new DeleteFolderResponseDto
//         {
//             Success = true,
//             Message = "Folder deleted successfully."
//         };
//     }

//     /// <summary>
//     /// Uploads a file to the specified folder.
//     /// </summary>
//     /// <param name="userId">The ID of the user uploading the file.</param>
//     /// <param name="request">The request DTO containing the file and folder ID.</param>
//     /// <returns>A response DTO with details about the uploaded file.</returns>
//     public async Task<UploadFileResponseDto?> UploadFileToFolderAsync(Guid userId, UploadFileRequestDto request)
//     {
//         var parentFolder = await _repository.GetFolderByIdAsync(request.ParentFolderId);
//         if (parentFolder is null || parentFolder.ParentUserId != userId)
//         {
//             throw new Exception("Folder not found or unauthorized.");
//         }

//         using var memoryStream = new MemoryStream();
//         await request.File.CopyToAsync(memoryStream);
//         var fileBytes = memoryStream.ToArray();

//         var newFile = new FileEntity
//         {
//             FileId = Guid.NewGuid(),
//             Filename = request.File.FileName,
//             FileContent = fileBytes,
//             ParentFolderId = request.ParentFolderId,
//             ParentFolder = parentFolder
//         };

//         var savedFile = await _repository.SaveFileAsync(newFile);

//         return new UploadFileResponseDto
//         {
//             FileId = savedFile.FileId,
//             Filename = savedFile.Filename
//         };
//     }

//     /// <summary>
//     /// Downloads a file by ID if the user is authorized to access it.
//     /// </summary>
//     /// <param name="userId">The ID of the user requesting the file.</param>
//     /// <param name="fileId">The ID of the file to download.</param>
//     /// <returns>A response DTO containing the file content and metadata, or null if unauthorized or not found.</returns>
//     public async Task<DownloadFileResponseDto?> DownloadFileFromFolderAsync(Guid userId, Guid fileId)
//     {
//         var file = await _repository.GetFileByIdAsync(fileId);

//         if (file is null || file.ParentFolder?.ParentUserId != userId)
//         {
//             return null;
//         }

//         return new DownloadFileResponseDto
//         {
//             FileId = file.FileId,
//             Filename = file.Filename,
//             Content = file.FileContent
//         };
//     }

//     /// <summary>
//     /// Updates the name of a file if the user is authorized.
//     /// </summary>
//     /// <param name="userId">The ID of the user requesting the update.</param>
//     /// <param name="fileId">The ID of the file to rename.</param>
//     /// <param name="newFilename">The new filename.</param>
//     /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
//     public async Task<bool> UpdateFileNameAsync(Guid userId, Guid fileId, string newFilename)
//     {
//         var file = await _repository.GetFileByIdAsync(fileId);

//         if (file is null || file.ParentFolder?.ParentUserId != userId)
//         {
//             return false;
//         }

//         file.Filename = newFilename;
//         await _repository.UpdateFileAsync(file);

//         return true;
//     }

//     /// <summary>
//     /// Deletes a file if the user is authorized.
//     /// </summary>
//     /// <param name="userId">The ID of the user deleting the file.</param>
//     /// <param name="fileId">The ID of the file to delete.</param>
//     /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
//     public async Task<bool> DeleteFileAsync(Guid userId, Guid fileId)
//     {
//         var file = await _repository.GetFileByIdAsync(fileId);

//         if (file is null || file.ParentFolder?.ParentUserId != userId)
//         {
//             return false;
//         }

//         await _repository.DeleteFileAsync(file);
//         return true;
//     }
// }
