public class DownloadFileFromFolderResponseDto
{
    public required Guid FileId { get; set; }
    public required string Filename { get; set; }
    public required byte[] FileContent { get; set; }
}
