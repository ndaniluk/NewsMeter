namespace NewsMeter.Core.Entities.Domain;

public class EmailConfiguration : ChannelConfiguration
{
    public List<string> Emails { get; set; } = [];
}