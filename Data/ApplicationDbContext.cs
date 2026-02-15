using Microsoft.EntityFrameworkCore;
using PromptHub.Models;

namespace PromptHub.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PromptTag> PromptTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure PromptTag as a composite key
        modelBuilder.Entity<PromptTag>()
            .HasKey(pt => new { pt.PromptId, pt.TagId });

        // Configure relationships
        modelBuilder.Entity<PromptTag>()
            .HasOne(pt => pt.Prompt)
            .WithMany(p => p.PromptTags)
            .HasForeignKey(pt => pt.PromptId);

        modelBuilder.Entity<PromptTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PromptTags)
            .HasForeignKey(pt => pt.TagId);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Finance", Description = "การเงิน", CreatedAt = DateTime.Now },
            new Category { Id = 2, Name = "IT/Technology", Description = "เทคโนโลยีสารสนเทศ", CreatedAt = DateTime.Now },
            new Category { Id = 3, Name = "Marketing", Description = "การตลาด", CreatedAt = DateTime.Now },
            new Category { Id = 4, Name = "Education", Description = "การศึกษา", CreatedAt = DateTime.Now },
            new Category { Id = 5, Name = "Creative Writing", Description = "การเขียนเชิงสร้างสรรค์", CreatedAt = DateTime.Now },
            new Category { Id = 6, Name = "Business", Description = "ธุรกิจ", CreatedAt = DateTime.Now },
            new Category { Id = 7, Name = "Health", Description = "สุขภาพ", CreatedAt = DateTime.Now },
            new Category { Id = 8, Name = "General", Description = "ทั่วไป", CreatedAt = DateTime.Now }
        );
    }
}
