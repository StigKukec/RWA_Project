namespace IntegrationModule.Models
{
    public class MVideoCreate
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Image { get; set; }

        public int TotalTime { get; set; }

        public string StreamingUrl { get; set; } = null!;
    }
}
