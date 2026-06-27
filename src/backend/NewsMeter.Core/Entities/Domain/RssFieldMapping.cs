namespace NewsMeter.Core.Entities.Domain;

public class RssFieldMapping
{
    public string Title { get; set; } = "title";
    public string Link { get; set; } = "link";
    public string PubDate { get; set; } = "pubDate";
    public string PubDateFormat { get; set; } = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
    public string Guid { get; set; } = "guid";
    public string Description { get; set; } = "description";
    public string Source { get; set; } = "source";
}