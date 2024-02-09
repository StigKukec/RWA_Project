using System.ComponentModel;

namespace RWAMovies.ViewModels
{
    public class VMUserProfile
    {
        [DisplayName("Created at")]
        public DateTime Created { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last name")]
        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        [DisplayName("E-mail")]
        public string Email { get; set; } = null!;

        public bool Verified { get; set; }

        public string Password { get; set; }

        public virtual VMCountry Country { get; set; } = null!;

        public virtual ICollection<VMUserNotification> UserNotifications { get; set; } = new List<VMUserNotification>();

        [DisplayName("User type")]
        public virtual VMUserType UserType { get; set; } = null!;
    }
}
