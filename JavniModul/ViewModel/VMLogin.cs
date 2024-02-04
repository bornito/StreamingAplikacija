using System.ComponentModel;

namespace JavniModul.ViewModel
{
    public class VMLogin
    {
        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Stay Signed-in")]
        public bool StaySignedIn { get; set; }
        public string? RedirectUrl { get; set; }
    }
}
