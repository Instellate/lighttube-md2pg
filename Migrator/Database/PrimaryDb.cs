using Migrator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Migrator.Database;

public class PrimaryDb : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;
    public DbSet<Login> Logins { get; set; } = null!;
    public DbSet<VideoCache> VideoCache { get; set; } = null!;
    public DbSet<Channel> Channels { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<PlaylistVideoID> PlaylistVideoIDs { get; set; } = null!;
    public DbSet<OAuthToken> OAuthTokens { get; set; } = null!;
}