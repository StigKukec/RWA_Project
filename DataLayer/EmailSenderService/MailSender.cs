using Microsoft.Extensions.Options;

namespace DataLayer.EmailSenderService
{
    public abstract class MailSender
    {
        public const string ConfigurationKey = "SMTPConfiguration";
        public abstract string Host { get; set; }
        public abstract int Port { get; set; }
    }
}
