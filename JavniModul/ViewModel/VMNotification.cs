using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMNotification
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "The Reciver Email field is required.")]
        [StringLength(256)]
        public string Receiver { get; set; } = null!;

        public string Subject { get; set; }


        [Required(ErrorMessage = "The Body field is required.")]
        [StringLength(1023)]
        public string EmailBody { get; set; } = null!;

        public DateTime? Sent { get; set; }
        public DateTime Created { get; set; }
    }
}
