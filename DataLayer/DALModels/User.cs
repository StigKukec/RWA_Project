using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class User
{
    public int Iduser { get; set; }

    public DateTime Created { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Verified { get; set; }

    public string PwdSalt { get; set; } = null!;

    public string PwdHash { get; set; } = null!;

    public string SecurityToken { get; set; } = null!;

    public int CountryId { get; set; }

    public DateTime? DeactivatedAt { get; set; }

    public bool Deactivated { get; set; }

    public int UserTypeId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

    public virtual UserType UserType { get; set; } = null!;
}
