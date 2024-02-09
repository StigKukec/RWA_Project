using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RWAMovies.ViewModels
{
    public class VMUserCreate
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
        [Compare(nameof(Email))]
        [DisplayName("E-mail confirmation")]
        public string EmailConfirm { get; set; } = null!;

        public bool Verify { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        [PasswordPropertyText]
        [Compare(nameof(Password))]
        [DisplayName("Password confirmation")]
        public string PasswordConfirm { get; set; }

        public virtual VMCountry Country { get; set; } = null!;

        public bool Deactivate { get; set; }

        [DisplayName("User type")]
        public virtual VMUserType UserType { get; set; } = null!;
    }
}
