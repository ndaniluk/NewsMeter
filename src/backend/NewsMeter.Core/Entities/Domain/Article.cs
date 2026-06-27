namespace NewsMeter.Core.Entities.Domain;

public class Article
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Link { get; set; }
    public DateTimeOffset PubDate { get; set; }
    public required string Source { get; set; }
    public DateTimeOffset FetchedAt { get; set; }

    public Guid PhraseId { get; set; }
    public Phrase Phrase { get; set; } = null!;
}