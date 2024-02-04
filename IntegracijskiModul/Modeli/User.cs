using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Modeli
{
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string? TelephoneNumber { get; set; }

        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? Token { get; set; }

        public bool IsConfirmed { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }


        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;

    }
}
