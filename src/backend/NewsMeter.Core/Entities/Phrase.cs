namespace NewsMeter.Core.Entities;

public class Phrase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string KeyPhrase { get; set; }
    public bool IsActive { get; set; }
    public decimal? Multiplier { get; set; }
    public int? MinAbsolute { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public ICollection<Article> Articles { get; set; } = [];
    public ICollection<AlertRule> AlertRules { get; set; } = [];
}