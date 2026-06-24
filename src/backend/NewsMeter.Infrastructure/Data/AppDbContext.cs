using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewsMeter.Core.Entities;
using System.Text.Json;


namespace NewsMeter.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AlertRule> AlertRules { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<GlobalSettings> GlobalSettings { get; set; }
    public DbSet<NotificationChannel> NotificationChannels { get; set; }
    public DbSet<Phrase> Phrases { get; set; }
    public DbSet<RssSource> RssSources { get; set; }
    public DbSet<SpikeEvent> SpikeEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var guidListComparer = new ValueComparer<List<Guid>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        modelBuilder.Entity<AlertRule>()
            .Property(e => e.NotificationChannelIds)
            .HasColumnType("TEXT")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null)!)
            .Metadata.SetValueComparer(guidListComparer);

        modelBuilder.Entity<GlobalSettings>()
            .Property(e => e.DefaultNotificationChannelIds)
            .HasColumnType("TEXT")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null)!)
            .Metadata.SetValueComparer(guidListComparer);

        modelBuilder.Entity<SpikeEvent>()
            .Property(e => e.ChannelsNotified)
            .HasColumnType("TEXT")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null)!)
            .Metadata.SetValueComparer(guidListComparer);

        modelBuilder.Entity<NotificationChannel>()
             .Property(e => e.Configuration)
             .HasColumnType("TEXT")
             .HasConversion(
                 v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                 v => JsonSerializer.Deserialize<ChannelConfiguration>(v, (JsonSerializerOptions?)null)!)
             .Metadata.SetValueComparer(new ValueComparer<ChannelConfiguration>(
                 (c1, c2) => JsonSerializer.Serialize(c1, (JsonSerializerOptions?)null) == JsonSerializer.Serialize(c2, (JsonSerializerOptions?)null),
                 c => JsonSerializer.Serialize(c, (JsonSerializerOptions?)null).GetHashCode(),
                 c => JsonSerializer.Deserialize<ChannelConfiguration>(JsonSerializer.Serialize(c, (JsonSerializerOptions?)null), (JsonSerializerOptions?)null)!));

        modelBuilder.Entity<RssSource>()
            .OwnsOne(e => e.FieldMapping, b => b.ToJson());

        var dictComparer = new ValueComparer<Dictionary<string, string>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => new Dictionary<string, string>(c));

        modelBuilder.Entity<RssSource>()
            .Property(e => e.ParameterValues)
            .HasColumnType("TEXT")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!)
            .Metadata.SetValueComparer(dictComparer);

        modelBuilder.Entity<Article>()
            .HasIndex(e => e.Link)
            .IsUnique();

        modelBuilder.Entity<Article>()
            .HasIndex(e => new { e.PhraseId, e.PubDate });

        modelBuilder.Entity<SpikeEvent>()
            .HasIndex(e => new { e.PhraseId, e.Date });
    }
}

