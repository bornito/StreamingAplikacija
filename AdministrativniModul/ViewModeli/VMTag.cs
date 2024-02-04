using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMTag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string Name { get; set; } = null!;

        public virtual ICollection<VMVideoTag> VideoTags { get; set; } = new List<VMVideoTag>();
    }
}
