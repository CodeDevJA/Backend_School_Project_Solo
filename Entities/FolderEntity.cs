using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FolderEntity
{
    [Key]
    public Guid FolderId { get; set; }

    [Required]
    public required string Foldername { get; set; }

    // Folders in Folder
    public ICollection<FolderEntity>? Folders { get; set; } = new List<FolderEntity>();

    // Files in Folder
    [Required]
    public required ICollection<FileEntity> Files { get; set; } = new List<FileEntity>();


    // ParentFolder
    public Guid? ParentFolderId { get; set; }
    [ForeignKey(nameof(ParentFolderId))]
    public FolderEntity? ParentFolder { get; set; }

    // UserAccount
    [Required]
    public required Guid ParentUserId { get; set; }
    [ForeignKey(nameof(ParentUserId))]
    public required UserEntity ParentUser { get; set; }


    // Constructor (Manual)
    public FolderEntity(string foldername, Guid parentFolderId, FolderEntity parentFolder, Guid parentUserId, UserEntity parentUser)
    {
        this.FolderId = Guid.NewGuid();
        this.Foldername = foldername;
        this.Folders = new List<FolderEntity>();
        this.Files = new List<FileEntity>();
        this.ParentFolderId = parentFolderId;
        this.ParentFolder = parentFolder;
        this.ParentUserId = parentUserId;
        this.ParentUser = parentUser;
    }

    // Constructor (Empty for EF)
    public FolderEntity()
    {
        this.Foldername = string.Empty;
        this.Folders = new List<FolderEntity>();
        this.Files = new List<FileEntity>();
        this.ParentFolderId = Guid.Empty;
        this.ParentUserId = Guid.Empty;
    }
}
