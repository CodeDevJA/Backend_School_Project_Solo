// DTO - Request
public class UploadFileRequestDto
{
    public required Guid ParentFolderId { get; set; }
    public required IFormFile File { get; set; } // IFormFile -> Stream -> Byte[]
}

// DTO - Response
public class UploadFileResponseDto
{
    public Guid FileId { get; set; }
    public required string Filename { get; set; }
    public required Stream FileStream { get; set; } // Byte[] -> Stream -> IFormFile
}
