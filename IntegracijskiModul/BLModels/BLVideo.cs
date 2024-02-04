using IntegracijskiModul.Modeli;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.BLModels
{
    public class BLVideo
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int? ImageId { get; set; }

        public virtual BLImage? Image { get; set; }

        [DisplayName("Total time")]
        public int TotalTime { get; set; }

        [Url]
        [DisplayName("Link")]
        public string? StreamingUrl { get; set; }

        public int GenreId { get; set; }

        public virtual BLGenre Genre { get; set; } = null!;

        public virtual ICollection<BLVideoTag> VideoTags { get; set; } = new List<BLVideoTag>();

        public List<Tag> Tags { get; set; } = null!;

        [DisplayName("Created at")]
        public DateTime Created { get; set;  } //= DateTime.Now;
    }
}
