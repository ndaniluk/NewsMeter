using NewsMeter.Core.Enums;

namespace NewsMeter.Core.Entities;

public class NotificationChannel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public NotificationChannelType Type { get; set; }
    public required ChannelConfiguration Configuration { get; set; }
    public bool IsReusable { get; set; }
    public bool IsActive { get; set; }
}