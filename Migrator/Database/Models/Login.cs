namespace Migrator.Database.Models;

public class Login
{
    public required string Id { get; set; }
    public required User User { get; set; }
    public required string Token { get; set; }
    public required string UserAgent { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.MinValue;
    public DateTimeOffset LastSeen { get; set; } = DateTimeOffset.MinValue;
}