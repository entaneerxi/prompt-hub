using System.ComponentModel.DataAnnotations;

namespace PromptHub.Models;

public class Tag
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public ICollection<PromptTag> PromptTags { get; set; } = new List<PromptTag>();
}
