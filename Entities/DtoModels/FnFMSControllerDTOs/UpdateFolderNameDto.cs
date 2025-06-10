public class UpdateFolderNameRequestDto
{
    public required string NewFolderName { get; set; }
}

public class UpdateFolderNameResponseDto
{
    public required Guid FolderId { get; set; }
    public required string UpdatedFolderName { get; set; }
}
