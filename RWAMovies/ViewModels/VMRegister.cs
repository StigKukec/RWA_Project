using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RWAMovies.ViewModels
{
    public class VMRegister
    {
        [DisplayName("First name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last name")]
        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; } = null!;

        [EmailAddress]
        [DisplayName("E-mail confirmation")]
        [Compare("Email")]
        public string? EmailConfirm { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        [PasswordPropertyText]
        [DisplayName("Password confirmation")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public VMCountry Country { get; set; }

    }
}
