public class UpdateFileNameRequestDto
{
    public required string NewFilename { get; set; }
}

public class UpdateFileNameResponseDto
{
    public required Guid FileId { get; set; }
    public required string UpdatedFilename { get; set; }
}
