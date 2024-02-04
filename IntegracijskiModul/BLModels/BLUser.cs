using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.BLModels
{
    public class BLUser
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? TelephoneNumber { get; set; }

        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? Token { get; set; }

        public bool IsConfirmed { get; set; }

        [DisplayName("Created at ")]
        public DateTime Created { get; set; }

        [DisplayName("Deleted at ")]
        public DateTime? Deleted { get; set; }


        public int CountryId { get; set; } 
        public virtual BLCountry Country { get; set; } = null!;
    }
}
