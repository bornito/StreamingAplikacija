using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMVideo
    {
        public int Id { get; set; }

        [DisplayName("Viedo name")]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Obavezan unos!")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Obavezan unos!")]
        public int TotalTime { get; set; }

        [Url]
        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string StreamingUrl { get; set; }

        public int ImageId { get; set; }
        public virtual VMImage Image { get; set; }
        public string ImageContent { get; set; }
        public virtual VMImage VMImage { get; set; }

        public virtual VMGenre Genre { get; set; }

        [DisplayName("Genre name")]
        public string GenreName { get; set; }
        public virtual VMGenre VMGenre { get; set; } = null!;

        public virtual ICollection<VMVideoTag> VideoTags { get; set; } = new List<VMVideoTag>();
        public List<int> SelectedTagIds { get; set; }


        [DisplayName("Created at")]
        public DateTime Created { get; set; }
    }
}
