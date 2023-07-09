using Migrator.Mongo;

namespace Migrator.Database.Models;

public class UserSubscription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required User User { get; set; }
    public VideoCache Video { get; set; } = null!;
    public required string VideoId { get; set; }
    public required SubscriptionType Type { get; set; }
}