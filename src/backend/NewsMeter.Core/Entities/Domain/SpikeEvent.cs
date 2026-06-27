namespace NewsMeter.Core.Entities.Domain;

public class SpikeEvent
{
    public Guid Id { get; set; }
    public DateTimeOffset DetectedAt { get; set; }
    public DateOnly Date { get; set; }
    public int ArticleCount { get; set; }
    public decimal Threshold { get; set; }
    public decimal RollingAverage { get; set; }
    public List<Guid> ChannelsNotified { get; set; } = [];

    public Guid AlertRuleId { get; set; }
    public AlertRule AlertRule { get; set; } = null!;

    public Guid PhraseId { get; set; }
    public Phrase Phrase { get; set; } = null!;
}