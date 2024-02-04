using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Modeli
{
    public partial class Image
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();

    }
}
