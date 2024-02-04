using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMUser
    {
        public int Id { get; set; }

        [DisplayName("User name:")]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string UserName { get; set; } = null!;

        [DisplayName("First name:")]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last name:")]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string PasswordSalt { get; set; } = null!;

        [Phone]
        public string Telephone { get; set; }

        public bool IsConfirmed { get; set; }

        public string Token { get; set; }

        [DisplayName("Country id")]
        public int CountryId { get; set; }
        [DisplayName("Country")]
        public virtual VMCountry Country { get; set; }


        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Deleted")]
        public DateTime? Deleted { get; set; }
    }
}
