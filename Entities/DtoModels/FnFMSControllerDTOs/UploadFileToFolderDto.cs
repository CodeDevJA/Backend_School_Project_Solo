public class UploadFileToFolderRequestDto
{
    public required string Filename { get; set; }
    public required byte[] FileContent { get; set; }
    public required Guid ParentFolderId { get; set; }
}

public class UploadFileToFolderResponseDto
{
    public required Guid FileId { get; set; }
    public required string Filename { get; set; }
    public required Guid ParentFolderId { get; set; }
}
