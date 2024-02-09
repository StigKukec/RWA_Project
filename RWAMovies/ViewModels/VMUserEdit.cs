using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RWAMovies.ViewModels
{
    public class VMUserEdit
    {
        [DisplayName("First name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last name")]
        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; } = null!;

        public bool Verified { get; set; }

        public bool Deactivated { get; set; }

        public virtual VMCountry Country { get; set; } = null!;

        [DisplayName("User type")]
        public virtual VMUserType UserType { get; set; } = null!;

    }
}
