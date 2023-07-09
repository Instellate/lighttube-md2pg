namespace Migrator.Database.Models;

public class OAuthToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string UserId { get; set; }
    public required string? ClientId { get; set; }
    public required string RefreshToken { get; set; }
    public required string CurrentAuthToken { get; set; }
    public required string Scopes { get; set; }
    public required DateTimeOffset CurrentTokenExpirationDate { get; set; }
}