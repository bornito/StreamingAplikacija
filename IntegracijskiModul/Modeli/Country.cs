using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Modeli
{
    public partial class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(2,MinimumLength =2)]
        public string CountryCode { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
