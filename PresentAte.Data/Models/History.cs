namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class History
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Presentation))]
        public int PresentationId { get; set; }

        [Required]
        public virtual Presentation Presentation { get; set; } = null!;
    }
}
