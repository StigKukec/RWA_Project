
using DataLayer.DALModels;

namespace DataLayer.BLModels
{
    public class BLUser
    {
        public int Iduser { get; set; }

        public DateTime Created { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool Verified { get; set; }

        public string Password { get; set; } = null!;

        public string SecurityToken { get; set; } = null!;

        public DateTime? DeactivatedAt { get; set; }

        public bool Deactivated { get; set; }

        public virtual BLCountry Country { get; set; } = null!;

        public virtual ICollection<BLUserNotification> UserNotifications { get; set; } = new List<BLUserNotification>();

        public virtual BLUserType UserType { get; set; } = null!;
    }
}
