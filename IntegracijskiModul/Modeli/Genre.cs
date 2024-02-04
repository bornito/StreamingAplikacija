using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Modeli
{
    public partial class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
