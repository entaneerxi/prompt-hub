using System.ComponentModel.DataAnnotations;

namespace PromptHub.Models;

public class Prompt
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    [Url]
    public string? SourceUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Category? Category { get; set; }
    public ICollection<PromptTag> PromptTags { get; set; } = new List<PromptTag>();
}
