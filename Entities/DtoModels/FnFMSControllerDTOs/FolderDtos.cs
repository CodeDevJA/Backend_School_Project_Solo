// DTO - Request
public class CreateRootFolderRequestDto
{
    public required string FolderName { get; set; }
}

public class CreateFolderInFolderRequestDto
{
    public required string FolderName { get; set; }

    public required Guid ParentFolderId { get; set; }
}

public class UpdateFolderNameRequestDto
{
    public required Guid FolderId { get; set; }

    public required string NewFolderName { get; set; }
}

// DTO - Response
public class FolderResponseDto
{
    public required Guid FolderId { get; set; }
    public required string FolderName { get; set; }
}
