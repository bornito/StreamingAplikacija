using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Modeli
{
    public partial class Video
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        [Required]
        public int? ImageId { get; set; }
        public virtual Image? Image { get; set; }

        [Required]
        public int TotalTime { get; set; }

        [Url]
        [Required]
        public string? StreamingUrl { get; set; }

        [Required]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();

        public DateTime Created { get; set; } // = DateTime.Now;


    }
}
