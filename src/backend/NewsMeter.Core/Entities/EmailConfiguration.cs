namespace NewsMeter.Core.Entities;

public class EmailConfiguration : ChannelConfiguration
{
    public List<string> Emails { get; set; } = [];
}