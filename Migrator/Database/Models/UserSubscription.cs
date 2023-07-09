using Migrator.Mongo;

namespace Migrator.Database.Models;

public class UserSubscription
{
    public required string Id { get; set; }
    public required SubscriptionType Type { get; set; }
}