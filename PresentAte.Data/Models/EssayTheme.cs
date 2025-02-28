using System.ComponentModel.DataAnnotations;

namespace PresentAte.Data.Models
{
    public class EssayTheme
    {
        [Key]
        public int ThemeId { get; set; }

        [Required]
        [StringLength(255)]
        public string ThemeName { get; set; }

        public ICollection<Essay> Essays { get; set; }
    }
}
