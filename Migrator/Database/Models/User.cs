namespace Migrator.Database.Models;

public class User
{
    public required string Id { get; set; }
    public required string PasswordHash { get; set; }
    public required List<UserSubscription> Subscriptions { get; set; }
    public required string LTChannelID { get; set; }
}