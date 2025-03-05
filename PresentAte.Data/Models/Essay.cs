namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class Essay
    {
        [Key]
        public int EssayId { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey("Theme")]
        public int ThemeId { get; set; }
        public EssayTheme Theme { get; set; } = null!;

        public ICollection<EssaySuggestion> Suggestions { get; set; }
            = new List<EssaySuggestion>();
        public ICollection<Comment> Comments { get; set; }
            = new HashSet<Comment>();
    }
}
