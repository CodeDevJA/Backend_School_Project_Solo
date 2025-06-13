using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class FileEntity
{
    [Key]
    public Guid FileId { get; set; }

    [Required]
    public required string Filename { get; set; }

    [Required]
    [JsonIgnore]
    public required byte[] FileContent { get; set; } = Array.Empty<byte>();


    // ParentFolder
    [Required]
    public Guid ParentFolderId { get; set; }
    [ForeignKey(nameof(ParentFolderId))]
    [Required]
    [JsonIgnore]
    public FolderEntity? ParentFolder { get; set; } // FolderEntity?, ? -> supresses issue in "Constructor (Empty for EF)"


    // Constructor (Manual)
    public FileEntity(string filename, byte[] fileContent, Guid parentFolderId, FolderEntity parentFolder)
    {
        this.FileId = Guid.NewGuid();
        this.Filename = filename;
        this.FileContent = fileContent;
        this.ParentFolderId = parentFolderId;
        this.ParentFolder = parentFolder;
    }
    
    // Constructor (Empty for EF)
    public FileEntity()
    {
        this.Filename = string.Empty;
        this.FileContent = Array.Empty<byte>();
        this.ParentFolderId = Guid.Empty;
    }
}
