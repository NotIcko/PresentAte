namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PresentAte.Common.ApplicationConstants.PresentationConstants;

    public class Presentation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PresentationTitleMaxLength)]
        public string Topic { get; set; } = null!;

        public string? HashCode { get; set; }

        public byte[] FileContent { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();
    }
}
