using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMGenre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezan unos!")]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1023)]
        public string Description { get; set; }

        public virtual ICollection<VMVideo> Videos { get; set; } = new List<VMVideo>();
    }
}
