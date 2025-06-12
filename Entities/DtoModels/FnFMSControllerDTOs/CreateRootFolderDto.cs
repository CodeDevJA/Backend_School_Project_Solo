public class CreateRootFolderRequestDto
{
    public required string FolderName { get; set; }
}

public class FolderResponseDto
{
    public Guid FolderId { get; set; }
    public string FolderName { get; set; } = string.Empty;
}
