using System.ComponentModel.DataAnnotations;

// DTO - Request
public class UploadFileRequestDto
{
    [Required]
    public required Guid ParentFolderId { get; set; }

    [Required]
    public required IFormFile File { get; set; } // IFormFile -> Stream -> Byte[]
}

// DTO - Response
public class UploadFileResponseDto
{
    public required Guid FileId { get; set; }
    public required string Filename { get; set; }
}

public class DownloadFileResponseDto
{
    public Guid FileId { get; set; }
    public string Filename { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
}

// DTO - Request
public class UpdateFileNameRequestDto
{
    public required Guid FileId { get; set; }
    public required string NewFilename { get; set; }
}

public class DeleteFileRequestDto
{
    public required Guid FileId { get; set; }
}
