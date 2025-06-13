using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class UserEntity : IdentityUser<Guid> // Change IdentityUser<string> to IdentityUser<Guid>
{
    [JsonIgnore]
    public ICollection<FolderEntity> Folders { get; set; } = new List<FolderEntity>();

    public UserEntity(ICollection<FolderEntity> folders)
    {
        this.Folders = new List<FolderEntity>();
    }

    public UserEntity()
    {
        this.Folders = new List<FolderEntity>();
    }
}
