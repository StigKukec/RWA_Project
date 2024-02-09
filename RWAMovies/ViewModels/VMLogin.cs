using System.ComponentModel;

namespace RWAMovies.ViewModels
{
    public class VMLogin
    {
        [DisplayName("User name")]
        public string Username { get; set; }

        [PasswordPropertyText]
        [DisplayName("Password")]
        public string Password { get; set; }

    }
}
