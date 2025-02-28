using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PresentAte.Data.Models
{
    public class EssaySuggestion
    {
        [Key]
        public int SuggestionId { get; set; }

        [Required]
        public string SuggestionText { get; set; }

        [ForeignKey("Essay")]
        public string EssayId { get; set; }
        public Essay Essay { get; set; }
    }
}
