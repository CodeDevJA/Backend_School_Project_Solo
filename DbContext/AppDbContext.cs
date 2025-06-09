using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Ensure identity configurations are applied

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(login => new { login.LoginProvider, login.ProviderKey }); // Define composite primary key

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Folders)
            .WithOne(fo => fo.ParentUser)
            .HasForeignKey(fo => fo.ParentUserId);

        modelBuilder.Entity<FolderEntity>()
            .HasOne(fo => fo.ParentFolder)
            .WithMany(fo => fo.Folders)
            .HasForeignKey(fo => fo.ParentFolderId)
            .HasPrincipalKey(fo => fo.FolderId);

        modelBuilder.Entity<FileEntity>()
            .HasOne(fi => fi.ParentFolder)
            .WithMany(fo => fo.Files)
            .HasForeignKey(fi => fi.ParentFolderId);
    }
}
