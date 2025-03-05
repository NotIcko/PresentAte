namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class EssaySuggestion
    {
        [Key]
        public int SuggestionId { get; set; }

        [Required]
        public string SuggestionText { get; set; }

        [ForeignKey("Essay")]
        public int EssayId { get; set; }
        public Essay Essay { get; set; }
    }
}
