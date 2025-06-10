public class CreateRootFolderRequestDto
{
    public required string FolderName { get; set; }
}

public class CreateRootFolderResponseDto
{
    public required Guid FolderId { get; set; }
    public required string FolderName { get; set; }
}
