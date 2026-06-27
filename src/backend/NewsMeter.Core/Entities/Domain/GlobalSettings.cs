namespace NewsMeter.Core.Entities.Domain;

public class GlobalSettings
{
    public Guid Id { get; set; }
    public List<Guid> DefaultNotificationChannelIds { get; set; } = [];
    public decimal DefaultMultiplier { get; set; } = 3.0m;
    public int DefaultMinAbsolute { get; set; } = 3;
    public int FetchIntervalMinutes { get; set; } = 60;
    public int RetentionDays { get; set; } = 30;
    public required string DefaultLanguage { get; set; }
    public required string Timezone { get; set; }
}