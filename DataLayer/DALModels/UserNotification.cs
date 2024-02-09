using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class UserNotification
{
    public int IduserNotification { get; set; }

    public int UserId { get; set; }

    public int NotificationId { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
