namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PresentAte.Common.ApplicationConstants.ThemeEssay;

    public class EssayTheme
    {
        [Key]
        public int ThemeId { get; set; }

        [Required]
        [StringLength(ThemeNameLength)]
        public string ThemeName { get; set; }

        public ICollection<Essay> Essays { get; set; }
    }
}
