public class CreateRootFolderRequestDto
{
    public required string FolderName { get; set; }
}

public class CreateFolderInFolderRequestDto
{
    public required string FolderName { get; set; }

    public required Guid ParentFolderId { get; set; }
}

public class FolderResponseDto
{
    public required Guid FolderId { get; set; }
    public required string FolderName { get; set; }
}
