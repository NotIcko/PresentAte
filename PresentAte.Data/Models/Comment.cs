namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static PresentAte.Common.ApplicationConstants.CommentConstants;

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(CommentContentLengthMax, ErrorMessage = $"The comment content cannot exceed 1000 characters.")]
        [Display(Name = "Comment Content")] 
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string EssayId { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("EssayId")]
        public Essay Essay { get; set; }
    }
}
