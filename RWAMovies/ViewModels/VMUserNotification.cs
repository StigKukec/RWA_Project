
namespace RWAMovies.ViewModels
{
    public class VMUserNotification
    {
        public int IduserNotification { get; set; }

        public int UserId { get; set; }

        public int NotificationId { get; set; }

        public virtual VMNotification Notification { get; set; } = null!;

        public virtual VMUser User { get; set; } = null!;
    }
}
