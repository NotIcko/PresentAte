using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PresentAte.Data.Models
{
    public class Essay
    {
        [Key]
        public string EssayId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Theme")]
        public int ThemeId { get; set; }
        public EssayTheme Theme { get; set; }

        public ICollection<EssaySuggestion> Suggestions { get; set; }
    }
}
