// DTO - Request
public class UploadFileToFolderRequestDto
{
    public required string Filename { get; set; }
    public required byte[] FileContent { get; set; }
    public required Guid ParentFolderId { get; set; }
}

// DTO - Response
public class UploadFileToFolderResponseDto
{
    public required Guid FileId { get; set; }
    public required string Filename { get; set; }
    public required Guid ParentFolderId { get; set; }
}

// DTO - Response
public class DownloadFileFromFolderResponseDto
{
    public required Guid FileId { get; set; }
    public required string Filename { get; set; }
    public required byte[] FileContent { get; set; }
}

// DTO - Request
public class UpdateFileNameRequestDto
{
    public required string NewFilename { get; set; }
}

// DTO - Response
public class UpdateFileNameResponseDto
{
    public required Guid FileId { get; set; }
    public required string UpdatedFilename { get; set; }
}

// DTO - Response
public class DeleteFileResponseDto
{
    public required Guid FileId { get; set; }
    public required bool IsDeleted { get; set; }
}
