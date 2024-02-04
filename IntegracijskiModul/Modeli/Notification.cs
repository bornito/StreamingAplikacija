using System.ComponentModel.DataAnnotations;

namespace IntegracijskiModul.Models
{
    public partial class Notification
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Reciever { get; set; } = null!;

        [MaxLength(128)]
        public string? Subject { get; set; }

        [MaxLength(1024)]
        public string? EmailBody { get; set; } 

        public DateTime? Created { get; set; }
        public DateTime? Sent { get; set; }
    }
}
