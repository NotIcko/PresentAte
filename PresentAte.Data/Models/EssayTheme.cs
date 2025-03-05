namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PresentAte.Common.ApplicationConstants.ThemeEssay;

    public class EssayTheme
    {
        [Key]
        public int ThemeId { get; set; }

        [Required]
        [MaxLength(ThemeNameLength)]
        public string ThemeName { get; set; } = null!;

        public ICollection<Essay> Essays { get; set; } = new List<Essay>();
    }
}
