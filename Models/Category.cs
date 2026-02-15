using System.ComponentModel.DataAnnotations;

namespace PromptHub.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();
}
