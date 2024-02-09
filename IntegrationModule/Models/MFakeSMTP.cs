using DataLayer.EmailSenderService;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace IntegrationModule.Models
{
    public class MFakeSMTP : MailSender
    {
        public const string FakeSMTP = $"{ConfigurationKey}:FakeSMTP";

        public override string Host { get; set; }
        public override int Port { get; set; }
    }
}
