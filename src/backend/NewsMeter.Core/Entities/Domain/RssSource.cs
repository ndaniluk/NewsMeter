namespace NewsMeter.Core.Entities.Domain;

public class RssSource
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string UrlTemplate { get; set; }
    public Dictionary<string, string> ParameterValues { get; set; } = [];
    public required RssFieldMapping FieldMapping { get; set; }
    public bool IsBuiltIn { get; set; }
    public bool IsActive { get; set; }
}