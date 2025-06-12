# TodoList
- Fix all the files related to Folders and Files. 
- Also fix the relations between diffirent datatypes. 
- 

## Request DTOs
- CreateRootFolderRequest
- CreateFolderInFolderRequest
- UploadFileToFolderRequest
- UpdateFolderNameRequest
- UpdateFileNameRequest
- DeleteFolderRequest
- DeleteFileRequest
- 

## Response DTOs
- FolderResponse
- FileResponse
- GetListOfAllFoldersResponse
- GetListOfAllFilesResponse
- GetListOfAllFoldersAndFilesResponse
- DownloadFileFromFolderResponse
- 

## Repository Interface (IFnFMSRepository.cs)
Folder Operations:
- Task<Guid> CreateRootFolderAsync(FolderEntity folder)
- Task<Guid> CreateFolderInFolderAsync(FolderEntity folder)
- Task<bool> UpdateFolderNameAsync(Guid folderId, string newFolderName)
- Task<bool> DeleteFolderAsync(Guid folderId)
- Task<List<FolderEntity>> GetAllFoldersAsync()
- Task<List<FolderEntity>> GetFoldersAndFilesAsync()

File Operations:
- Task<Guid> UploadFileToFolderAsync(FileEntity file)
- Task<FileEntity?> DownloadFileFromFolderAsync(Guid fileId)
- Task<bool> UpdateFileNameAsync(Guid fileId, string newFileName)
- Task<bool> DeleteFileAsync(Guid fileId)
- Task<List<FileEntity>> GetAllFilesAsync()

## Service Interface (IFnFMSService.cs)
Folder Operations:
- Task<Guid> CreateRootFolderAsync(CreateRootFolderRequest request)
- Task<Guid> CreateFolderInFolderAsync(CreateFolderInFolderRequest request)
- Task<bool> UpdateFolderNameAsync(UpdateFolderNameRequest request)
- Task<bool> DeleteFolderAsync(DeleteFolderRequest request)
- Task<GetListOfAllFoldersResponse> GetAllFoldersAsync()
- Task<GetListOfAllFoldersAndFilesResponse> GetFoldersAndFilesAsync()

File Operations:
- Task<Guid> UploadFileToFolderAsync(UploadFileToFolderRequest request)
- Task<DownloadFileFromFolderResponse?> DownloadFileFromFolderAsync(Guid fileId)
- Task<bool> UpdateFileNameAsync(UpdateFileNameRequest request)
- Task<bool> DeleteFileAsync(DeleteFileRequest request)
- Task<GetListOfAllFilesResponse> GetAllFilesAsync()

## Custom Exeption Classes
Folder Exceptions:
- FolderNotFoundException
- FolderAlreadyExistsException
- FolderAccessDeniedException

File Exceptions:
- FileNotFoundException
- FileAlreadyExistsException
- FileAccessDeniedException

General Exception:
- OperationFailedException (for unexpected failures)

