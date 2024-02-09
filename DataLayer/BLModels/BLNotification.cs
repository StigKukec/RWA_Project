


namespace DataLayer.BLModels
{
    public class BLNotification
    {
        public int Idnotification { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Sender { get; set; } = null!;

        public string Receiver { get; set; } = null!;

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public DateTime? SentAt { get; set; }

        public virtual ICollection<BLUserNotification> UserNotifications { get; set; } = new List<BLUserNotification>();

    }
}
