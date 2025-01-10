namespace PresentAte.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static PresentAte.Common.ApplicationConstants.UserConstants;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public virtual ICollection<History> Histories { get; set; }
            = new HashSet<History>();
    }
}
