namespace Migrator.Database.Models;

public class PlaylistVideoID
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Playlist Playlist { get; set; }
    public required VideoCache Video { get; set; }
}