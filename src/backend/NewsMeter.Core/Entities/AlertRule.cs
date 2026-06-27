using NewsMeter.Core.Enums;

namespace NewsMeter.Core.Entities;

public class AlertRule
{
    public Guid Id { get; set; }
    public AlertRuleType Type { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> NotificationChannelIds { get; set; } = [];

    public Guid PhraseId { get; set; }
    public Phrase Phrase { get; set; } = null!;
}