using Migrator.Mongo;

namespace Migrator.Database.Models;

public class UserSubscription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required User User { get; set; }
    public Channel Channel { get; set; } = null!;
    public required string ChannelId { get; set; }
    public required SubscriptionType Type { get; set; }
}