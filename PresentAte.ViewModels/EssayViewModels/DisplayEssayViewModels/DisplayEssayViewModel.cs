public class DisplayEssayViewModel
{
    public int EssayId { get; set; } // Unique identifier for the essay
    public string Content { get; set; } // The content of the essay
    public string UserName { get; set; } // The name of the user who submitted the essay
    public string ThemeName { get; set; } // The name of the theme the essay belongs to
    public DateTime CreatedAt { get; set; } // The date and time the essay was submitted
    public List<CommentViewModel> Comments { get; set; } // List of comments on the essay
}
