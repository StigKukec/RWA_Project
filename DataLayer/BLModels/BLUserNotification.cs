using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BLModels
{
    public class BLUserNotification
    {
        public int IduserNotification { get; set; }

        public int UserId { get; set; }

        public int NotificationId { get; set; }

        public virtual BLNotification Notification { get; set; } = null!;

        public virtual BLUser User { get; set; } = null!;
    }
}
